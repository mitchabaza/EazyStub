﻿using System;
using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using Latsos.Client;
using Latsos.Shared;
using Latsos.Shared.Response;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Latsos.Test.Client
{
    [TestFixture]
    public class ResponseBuilderFixture
    {

        [Test]
        public void Clear_ShouldResetAllProperties()
        {
            var builder = new ResponseBuilder(new StubBuilder());

             (builder.WithBody(Guid.NewGuid().ToString()).WithStatusCode(HttpStatusCode.Forbidden).WithHeader("header", "values") as ResponseBuilder).Build();

            builder.Clear();
            builder.Build().ShouldBeEquivalentTo(new HttpResponseModel());

        }

        [Test]
        public void Build_ShouldReturnModelWithAllProperties()
        {
            var builder = new ResponseBuilder(new StubBuilder());
            var responseModel =
                (builder.WithBody("buzz").WithStatusCode(HttpStatusCode.Forbidden).WithHeader("header", "values") as ResponseBuilder) .Build();

            responseModel.ShouldBeEquivalentTo(new HttpResponseModel()
            {
                Body = new Body() {Data = "buzz"},
                Headers = new Headers(new Dictionary<string, string>() { { "header", "values" } }),
                StatusCode = HttpStatusCode.Forbidden
            });

            builder = new ResponseBuilder(new StubBuilder());
            responseModel =
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