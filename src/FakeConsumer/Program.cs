using System;
using System.Net;
using EasyStub.Client;
using EasyStub.Common.Request;

namespace FakeConsumer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Settings.SetServerUrl("http://localhost/EasyStub");
            var builder = new StubBuilder();
            
            new StubChannel(Settings.Url).Reset();
            builder
                .AllRequests.WithPath("customer/add").AndMethod(Method.Post)
                .WillReturnResponse()
                .WithStatusCode(HttpStatusCode.Conflict)
                .WithBody(new {Message = "Customer Already Exists"}.ToJson())
                .BuildAndRegister();

            builder.AllRequests.WithPath("customer/2")
                .WillReturnResponse()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBody(new {Customer = "Jack Black", DOB = DateTime.Now.AddYears(-43)}.ToJson())
                .WithHeader("AuthToken", Guid.NewGuid().ToString())
                .BuildAndRegister();
        }
    }
}