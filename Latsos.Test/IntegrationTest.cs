using System;
using System.Net;
using System.Net.Http;
using FluentAssertions;
using Latsos.Client;
using Latsos.Shared;
using Newtonsoft.Json;
using NUnit.Framework;

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
            Console.WriteLine(JsonConvert.SerializeObject(behaviorRegistrationRequest2));

            stub.List().ShouldBeEquivalentTo(new[] { behaviorRegistrationRequest1, behaviorRegistrationRequest2 });

        }
    }
}