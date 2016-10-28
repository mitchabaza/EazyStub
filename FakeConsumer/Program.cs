using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Latsos.Client;

namespace FakeConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new StubBuilder();

            Settings.SetServerUrl("http://localhost/Latsos");
            new StubChannel(Settings.Url).Clear();

            builder.AllRequests.WithPath("customer/1")
                .WillReturnResponse()
                .WithStatusCode(HttpStatusCode.BadRequest)
                .WithBody(new {Message = "Bad Request Dumbass"}.ToJson())
                .WithHeader("Server", "IIS").WithHeader("X-Powered-By","asdasd")
                .Register();

            builder.AllRequests.WithPath("customer/2")
                .WillReturnResponse()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBody(new {Customer = "Jack Black", DOB = DateTime.Now.AddYears(-43)}.ToJson())
                .WithHeader("maggot", "maggot")
                .Register();
        }
    }
}