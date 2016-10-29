using System;
using System.Collections.Generic;
using System.Net;
using EasyStub.Client;
using EasyStub.Common;
using EasyStub.Common.Response;
using FluentAssertions;
using NUnit.Framework;

namespace EasyStub.Test.Client
{
    [TestFixture]
    public class ResponseBuilderFixture
    {
        [Test]
        public void Clear_ShouldResetAllProperties()
        {
            var builder = new ResponseBuilder(new StubBuilder());

            (builder.WithBody(Guid.NewGuid().ToString())
                .WithStatusCode(HttpStatusCode.Forbidden)
                .WithHeader("header", "values") as ResponseBuilder).Build();

            builder.Clear();
            builder.Build().ShouldBeEquivalentTo(new HttpResponseModel());
        }

        [Test]
        public void Build_ShouldReturnModelWithAllProperties1()
        {
            var builder = new ResponseBuilder(new StubBuilder());
            var responseModel =
                (builder.WithBody("buzz").WithStatusCode(HttpStatusCode.Forbidden).WithHeader("header", "values") as
                    ResponseBuilder).Build();

            responseModel.ShouldBeEquivalentTo(new HttpResponseModel()
            {
                Body = new Body() {Data = "buzz"},
                Headers = new Headers(new Dictionary<string, string>() {{"header", "values"}}),
                StatusCode = HttpStatusCode.Forbidden
            });
        }

        [Test]
        public void Build_ShouldReturnModelWithAllProperties2()
        {
            var builder = new ResponseBuilder(new StubBuilder());
            var responseModel =
                (builder.WithStatusCode(HttpStatusCode.Forbidden).WithBody("buzz").WithHeader("header", "values") as ResponseBuilder).Build();

            responseModel.ShouldBeEquivalentTo(new HttpResponseModel()
            {
                Body = new Body() { Data = "buzz" },
                Headers = new Headers(new Dictionary<string, string>() { { "header", "values" } }),
                StatusCode = HttpStatusCode.Forbidden
            });
        }
    }
}