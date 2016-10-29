using System;
using System.Net;
using System.Text;
using EasyStub.Client;
using EasyStub.Common;
using FluentAssertions;
using NUnit.Framework;
using Method = EasyStub.Common.Request.Method;

namespace EasyStub.Test
{
    [TestFixture]
    public class IntegrationFixture
    {
        private StubChannel _channel;

        [SetUp]
        public void SetUp()
        {
            Settings.SetServerUrl("http://localhost/EasyStub");
            _channel = new StubChannel(Settings.Url);
            _channel.Reset();
        }
        [Test]
        public void View_ShouldReturnNull_WhenNoRegistrations()
        {
            var client =new StubChannel(Settings.Url);
            client.List().ShouldBeEquivalentTo(new StubRegistration[0]);
        }

        [Test]
        public void Delete_ShouldRemoveAllRegistrations()
        {
             
            _channel.Register(new StubBuilder().AllRequests.WithPath("dance").WillReturnResponse().WithStatusCode(HttpStatusCode.Conflict).Build());
            _channel.List().Length.Should().BeGreaterOrEqualTo(1);
            _channel.Reset();
            _channel.List().ShouldBeEquivalentTo(new StubRegistration[0]);

        }

        [Test]
        public void Delete_ShoulOnlyDeleteProvidedRegistration()
        {
             _channel.Register(new StubBuilder().AllRequests.WithPath("path2").WillReturnResponse().WithStatusCode(HttpStatusCode.Conflict).Build());
            _channel.Register(new StubBuilder().AllRequests.WithPath("path1").WillReturnResponse().WithStatusCode(HttpStatusCode.Conflict).Build());
            _channel.List().Length.Should().Be(2);
            Console.WriteLine(new StubBuilder().AllRequests.WithPath("path2").Build().GetHashCode());
            Console.WriteLine(new StubBuilder().AllRequests.WithPath("path2").Build().GetHashCode());
            _channel.UnRegister(new StubBuilder().AllRequests.WithPath("path1").WillReturnResponse().WithStatusCode(HttpStatusCode.Conflict).Build());
            _channel.List().Length.Should().BeGreaterOrEqualTo(1);

        }

        [Test]
        public void MatchedRequest_ShouldReturnStubResponse()
        {
            var returnPayload = new {CustomerID=1, FirstName="Mitch", LastName="Abaza"}.ToJson();
            var client = new StubChannel(Settings.Url);
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

             var builder = new StubBuilder();
            var behaviorRegistrationRequest1 = builder.AllRequests
             
                .WithPath("test/method/1").WithMethod(Method.Get)
                .WithHeader("buzz","zz").WillReturnResponse()
                .WithStatusCode(HttpStatusCode.BadGateway)
                .Build();
            
            _channel.Register(behaviorRegistrationRequest1);
            _channel.List().ShouldBeEquivalentTo(new[] { behaviorRegistrationRequest1 });

            var behaviorRegistrationRequest2 = builder.AllRequests
               
                .WithPath("test/method/1").WithMethod(Method.Get).WillReturnResponse()
                .WithBody("mom","application/json", Encoding.UTF8.WebName)
                .Build();
            Console.WriteLine(behaviorRegistrationRequest2.ToJson());
            _channel.Register(behaviorRegistrationRequest2);

            _channel.List().ShouldBeEquivalentTo(new[] { behaviorRegistrationRequest1, behaviorRegistrationRequest2 });

        }
    }
}