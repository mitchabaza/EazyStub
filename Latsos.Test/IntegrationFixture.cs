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
        [Test]
        public void View_ShouldReturnNull_WhenNoRegistrations()
        {
            var client =new Factory();
            client.Clear();
            client.List().ShouldBeEquivalentTo(new StubRegistration[0]);
        }

        [Test]
        public void Delete_ShouldRemoveAllRegistrations()
        {
            var stub = new Factory();
            
            stub.Register(new StubBuilder().Request.WithPath("dance").ReturnsResponse.WithStatusCode(HttpStatusCode.Conflict).Build());
            stub.List().Length.Should().BeGreaterOrEqualTo(1);
            stub.Clear();
            stub.List().ShouldBeEquivalentTo(new StubRegistration[0]);

        }

        [Test]
        public void Delete_ShoulOnlyDeleteProvidedRegistration()
        {
            var stub = new Factory();
            stub.Clear();
            stub.Register(new StubBuilder().Request.WithPath("path2").ReturnsResponse.WithStatusCode(HttpStatusCode.Conflict).Build());
            stub.Register(new StubBuilder().Request.WithPath("path1").ReturnsResponse.WithStatusCode(HttpStatusCode.Conflict).Build());
            stub.List().Length.Should().Be(2);
            Console.WriteLine(new StubBuilder().Request.WithPath("path2").Build().GetHashCode());
            Console.WriteLine(new StubBuilder().Request.WithPath("path2").Build().GetHashCode());
            stub.UnRegister(new StubBuilder().Request.WithPath("path1").ReturnsResponse.WithStatusCode(HttpStatusCode.Conflict).Build());
            stub.List().Length.Should().BeGreaterOrEqualTo(1);

        }

        [Test]
        public void MatchedRequest_ShouldReturnStubResponse()
        {
            var returnPayload = new {CustomerID=1, FirstName="Mitch", LastName="Abaza"}.ToJson();
            var client = new Factory();
            var builder = new StubBuilder();
            
            var behaviorRegistrationRequest = builder.Request
                .WithPath("customer/get/1")
                .WithMethod(Method.Get)
                .ReturnsResponse
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
            var stub = new Factory();
            var builder = new StubBuilder();
            var behaviorRegistrationRequest1 = builder.Request
             
                .WithPath("test/method/1").WithMethod(Method.Get)
                .WithHeader("buzz","zz")
                .ReturnsResponse
                .WithStatusCode(HttpStatusCode.BadGateway)
                .Build();
            
            stub.Register(behaviorRegistrationRequest1);
            stub.List().ShouldBeEquivalentTo(new[] { behaviorRegistrationRequest1 });

            var behaviorRegistrationRequest2 = builder.Request
               
                .WithPath("test/method/1").WithMethod(Method.Get)
                .ReturnsResponse
                .WithBody("mom","application/json", Encoding.UTF8.WebName)
                .Build();
            Console.WriteLine(behaviorRegistrationRequest2.ToJson());
            stub.Register(behaviorRegistrationRequest2);

            stub.List().ShouldBeEquivalentTo(new[] { behaviorRegistrationRequest1, behaviorRegistrationRequest2 });

        }
    }
}