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
            builder.Request.Path("customer/1").Response.StatusCode(200).Build().Register();
            
        }
    }
}
