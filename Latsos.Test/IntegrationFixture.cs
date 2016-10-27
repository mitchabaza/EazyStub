using System;
using System.Net;
using System.Text;
using FluentAssertions;
using Latsos.Client;
using Latsos.Shared;
using Latsos.Test.Util;
using NUnit.Framework;
using Method = Latsos.Shared.Request.Method;

namespace Latsos.Test
{
    [TestFixture]
    public class IntegrationFixture
    {
        [SetUp]
        public void SetUp()
        {
            Settings.SetServerUrl("http://localhost/Latsos");
        }
        [Test]
        public void View_ShouldReturnNull_WhenNoRegistrations()
        {
            var client =new Factory(Settings.Url);
            client.Clear();
            client.List().ShouldBeEquivalentTo(new StubRegistration[0]);
        }

        [Test]
        public void Delete_ShouldRemoveAllRegistrations()
        {
            var stub = new Factory(Settings.Url);
            
            stub.Register(new StubBuilder().AllRequests.WithPath("dance").WillReturnResponse().WithStatusCode(HttpStatusCode.Conflict).Build());
            stub.List().Length.Should().BeGreaterOrEqualTo(1);
            stub.Clear();
            stub.List().ShouldBeEquivalentTo(new StubRegistration[0]);

        }

        [Test]
        public void Delete_ShoulOnlyDeleteProvidedRegistration()
        {
            var stub = new Factory(Settings.Url);
            stub.Clear();
            stub.Register(new StubBuilder().AllRequests.WithPath("path2").WillReturnResponse().WithStatusCode(HttpStatusCode.Conflict).Build());
            stub.Register(new StubBuilder().AllRequests.WithPath("path1").WillReturnResponse().WithStatusCode(HttpStatusCode.Conflict).Build());
            stub.List().Length.Should().Be(2);
            Console.WriteLine(new StubBuilder().AllRequests.WithPath("path2").Build().GetHashCode());
            Console.WriteLine(new StubBuilder().AllRequests.WithPath("path2").Build().GetHashCode());
            stub.UnRegister(new StubBuilder().AllRequests.WithPath("path1").WillReturnResponse().WithStatusCode(HttpStatusCode.Conflict).Build());
            stub.List().Length.Should().BeGreaterOrEqualTo(1);

        }

        [Test]
        public void MatchedRequest_ShouldReturnStubResponse()
        {
            var returnPayload = new {CustomerID=1, FirstName="Mitch", LastName="Abaza"}.ToJson();
            var client = new Factory(Settings.Url);
            var builder = new StubBuilder();
            
            var behaviorRegistrationRequest = builder.AllRequests
                .WithPath("customer/get/1")
                .WithMethod(Method.Get).WillReturnResponse()
                .WithStatusCode(HttpStatusCode.OK).WithBody(returnPayload, "application/json",Encoding.UTF8.WebName)
                .Build();

            Console.WriteLine(behaviorRegistrationRequest.ToJson());
            client.Register(behaviorRegistrationRequest);

           var response= client.Send(behaviorRegistrationRequest.Request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().Be(returnPayload);
            response.ContentType.Should().Be("application/json; charset=utf-8");
        }

        [Test]
        public void Add_ShouldWork()
        {
            var stub = new Factory(Settings.Url);
            var builder = new StubBuilder();
            var behaviorRegistrationRequest1 = builder.AllRequests
             
                .WithPath("test/method/1").WithMethod(Method.Get)
                .WithHeader("buzz","zz").WillReturnResponse()
                .WithStatusCode(HttpStatusCode.BadGateway)
                .Build();
            
            stub.Register(behaviorRegistrationRequest1);
            stub.List().ShouldBeEquivalentTo(new[] { behaviorRegistrationRequest1 });

            var behaviorRegistrationRequest2 = builder.AllRequests
               
                .WithPath("test/method/1").WithMethod(Method.Get).WillReturnResponse()
                .WithBody("mom","application/json", Encoding.UTF8.WebName)
                .Build();
            Console.WriteLine(behaviorRegistrationRequest2.ToJson());
            stub.Register(behaviorRegistrationRequest2);

            stub.List().ShouldBeEquivalentTo(new[] { behaviorRegistrationRequest1, behaviorRegistrationRequest2 });

        }
    }
}