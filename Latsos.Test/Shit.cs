using System;
using System.Net;
using System.Net.Http;
using Latsos.Client;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Latsos.Test
{
    [TestFixture]
    public class Shit
    {
        [Test]
        public void Ana()
        {
            var mock = new Stub();
            var json = new JsonSerializer();
            var builder = new MockBuilder();
            var buzz= builder.Request()
                .Method(HttpMethod.Get)
                .Path("cum/shit")
                .Returns()
                .StatusCode(HttpStatusCode.BadGateway)
                .Build();
            Console.Write(JsonConvert.SerializeObject(buzz));
        }
    }
}