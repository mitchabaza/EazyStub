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
           
           
            var stub =new StubClient();
            stub.Clear();
            stub.List().ShouldBeEquivalentTo(new StubRegistration[0]);
        }

        [Test]
        public void Delete_ShouldRemoveAllRegistrations()
        {


            var stub = new StubClient();
            stub.Add(new StubBuilder().Request().Path("dance").Returns().StatusCode(HttpStatusCode.Conflict).Build());
            stub.List().Length.Should().BeGreaterOrEqualTo(1);
            stub.Clear();
            stub.List().ShouldBeEquivalentTo(new StubRegistration[0]);

        }
        [Test]
        public void MatchedRequest_ShouldReturnStubResponse()
        {
            var returnPayload = new {CustomerID=1, FirstName="Mitch", LastName="Abaza"}.ToJson();
            var client = new StubClient();
            var builder = new StubBuilder();
            var behaviorRegistrationRequest = builder.Request()
                .Method(Method.Get)
                .Path("customer/get/1")
                .Returns()
                .StatusCode(HttpStatusCode.OK).Body(returnPayload, "application/json",Encoding.UTF8.WebName)
                .Build();

            Console.WriteLine(behaviorRegistrationRequest.ToJson());
            client.Add(behaviorRegistrationRequest);

           var response= client.Send(behaviorRegistrationRequest.Request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().Be(returnPayload);
            response.ContentType.Should().Be("application/json; charset=utf-8");
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
                .Body("mom","application/json", Encoding.UTF8.WebName)
                .Build();
            Console.WriteLine(behaviorRegistrationRequest2.ToJson());
            stub.Add(behaviorRegistrationRequest2);

            stub.List().ShouldBeEquivalentTo(new[] { behaviorRegistrationRequest1, behaviorRegistrationRequest2 });

        }
    }
}