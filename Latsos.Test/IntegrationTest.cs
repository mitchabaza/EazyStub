using System;
using System.Net;
using System.Net.Http;
using Latsos.Client;
using Latsos.Shared;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Latsos.Test
{
    [TestFixture]
    public class IntegrationTest
    {
        [Test]
        public void Simple()
        {
            var mock = new Stub();
            var json = new JsonSerializer();
            var builder = new MockBuilder();
            var buzz= builder.Request()
                .Method(Method.Get)
                .Path("test/method/1")
                .Returns()
                .StatusCode(HttpStatusCode.BadGateway)
                .Build();
            Console.Write(JsonConvert.SerializeObject(buzz));
        }
    }
}