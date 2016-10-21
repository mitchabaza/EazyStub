using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Results;
using FluentAssertions;
using Latsos.Client;
using Latsos.Shared;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using Method = Latsos.Shared.Method;

namespace Latsos.Test
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public void View_ShouldReturnNull_WhenNoRegistrations()
        {
           
           
            var stub =new StubClient();
            stub.Clear();
            stub.List().ShouldBeEquivalentTo(new StubRegistration[0]);
        }

        [Test]
        public void MatchedRequest_ShoulReturnStubResponse()
        {
            var returnPayload = new {CustomerID=1, FirstName="Mitch", LastName="Abaza"}.ToJson();
            var client = new StubClient();
            var builder = new StubBuilder();
            var behaviorRegistrationRequest = builder.Request()
                .Method(Method.Get)
                .Path("customer/get/1")
                .Returns()
                .StatusCode(HttpStatusCode.OK).Body(returnPayload, "application/json")
                .Build();

            Console.WriteLine(behaviorRegistrationRequest.ToJson());
            client.Add(behaviorRegistrationRequest);

           var response= client.Send(behaviorRegistrationRequest.Request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().Be(returnPayload);
            response.ContentType.Should().Be("application/json");
        }

        [Test]
        public void Add_ShouldWork()
        {
            var stub = new StubClient();
            var builder = new StubBuilder();
            var behaviorRegistrationRequest1 = builder.Request()
                .Method(Method.Get)
                .Path("test/method/1")
                .Header("buzz","zz")
                .Returns()
                .StatusCode(HttpStatusCode.BadGateway)
                .Build();
            
            stub.Add(behaviorRegistrationRequest1);
            stub.List().ShouldBeEquivalentTo(new[] { behaviorRegistrationRequest1 });

            var behaviorRegistrationRequest2 = builder.Request()
                .Method(Method.Get)
                .Path("test/method/1")
                .Returns()
                .Body("","application/json;charset=utf-8")
                .Build();
            stub.Add(behaviorRegistrationRequest2);

            stub.List().ShouldBeEquivalentTo(new[] { behaviorRegistrationRequest1, behaviorRegistrationRequest2 });

        }
    }
}