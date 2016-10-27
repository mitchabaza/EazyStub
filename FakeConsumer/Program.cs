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
            builder.AllRequests.WithPath("customer/1").WillReturnResponse().WithStatusCode(HttpStatusCode.BadRequest).WithBody(new { Message = "Bad Request Dumbass" }.ToJson()).WithHeader("maggot", "maggot").Register();

            builder.AllRequests.WithPath("customer/2").WillReturnResponse().WithStatusCode(HttpStatusCode.OK).WithBody(new {Customer="Jack Black", DOB=DateTime.Now.AddYears(-43)}.ToJson()).WithHeader("maggot", "maggot").Register();
        }
    }
}
