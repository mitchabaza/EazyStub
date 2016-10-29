using System;
using System.Net;
using EasyStub.Client;
using EasyStub.Common.Request;

namespace FakeConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new StubBuilder();

            Settings.SetServerUrl("http://localhost/EasyStub");
            new StubChannel(Settings.Url).Reset();

            builder.AllRequests.WithPath("customer/add").WithMethod(Method.Post)
                .WillReturnResponse()
                .WithStatusCode(HttpStatusCode.Conflict)
                .WithBody(new {Message = "Customer Already Exists"}.ToJson())
                .Register();

            builder.AllRequests.WithPath("customer/2")
                .WillReturnResponse()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBody(new {Customer = "Jack Black", DOB = DateTime.Now.AddYears(-43)}.ToJson())
                .WithHeader("AuthToken", Guid.NewGuid().ToString())
                .Register();

        }
    }
}