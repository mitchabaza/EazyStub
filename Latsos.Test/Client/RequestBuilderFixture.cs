using System;
using System.Collections.Generic;
using FluentAssertions;
using Latsos.Client;
using Latsos.Shared;
using Latsos.Shared.Request;
using Latsos.Test.Util;
using NUnit.Framework;

namespace Latsos.Test.Client
{
    public class RequestBuilderFixture
    {
        [Test]
        public void RequestBuilder_ShouldCreateRequest_WhenQueryStringSpecified()
        {
            var builder = new StubBuilder();

            var id = Guid.NewGuid().ToString();
            var registration = builder.Request
                .WithPath("buzz/dance")
                .WithQueryString("id", id).Build();

            registration.ShouldBeEquivalentTo(
                new RequestRegistration()
                {
                    Headers = MatchRule<Headers>.Default,
                    Port = MatchRule<int>.Default,
                    LocalPath = "/buzz/dance",
                    Method = MatchRule<Method>.Default,
                    Query = new MatchRule<string>($"?id={id}")
                });
        }

        [Test]
        public void RequestBuilder_ShouldCreateRequest_WhenQueryStringAndMethodSpecified()
        {
            var builder = new StubBuilder();

            var id = Guid.NewGuid().ToString();
            var registration = builder.Request
                .WithPath("etc/1").WithMethod(Method.Post).WithQueryString("id", id).WithQueryString("jack", "jill")
                .Build();

            registration.ShouldBeEquivalentTo(
                new RequestRegistration()
                {
                    Headers = MatchRule<Headers>.Default,
                    Port = MatchRule<int>.Default,
                    LocalPath = "/etc/1",
                    Method = new MatchRule<Method>(Method.Post),
                    Query = new MatchRule<string>($"?id={id}&jack=jill")
                });
        }

        [Test]
        public void RequestBuilder_ShouldCreateRequest_WhenQueryStringAndMethodAndPortSpecified()
        {
            var builder = new StubBuilder();

            var id = Guid.NewGuid().ToString();
            var registration = builder.Request
                .WithPath("customer/delete/645564")
                .WithMethod(Method.Post)
                .WithQueryString("tranId", id)
                .WithQueryString("orgId", "64556456456")
                .WithQueryString("custid", "654654")
                .WithPort(9999)
                .Build();

            registration.ShouldBeEquivalentTo(
                new RequestRegistration()
                {
                    Headers = MatchRule<Headers>.Default,
                    Port = new MatchRule<int>(9999),
                    LocalPath = "/customer/delete/645564",
                    Method = new MatchRule<Method>(Method.Post),
                    Query = new MatchRule<string>($"?tranId={id}&orgId=64556456456&custid=654654")
                });
        }


        [Test]
        public void Build_ShouldResetInternalState()
        {
            var builder = new RequestBuilder(new StubBuilder());
             builder
                .WithPath("test/method/1")
                .WithMethod(Method.Get).WithQueryString("customerId", "153").WithQueryString("orderId", "1314")
                .WithBody("<Html/>", "text/html")
                .WithHeader("X-Powered-By", "IIS")
                .WithPort(443)
                .Build();

            var request2 = builder.WithPath("test/method/1").Build();

            request2.ShouldEqual(new RequestRegistration() {LocalPath = "test/method/1" });
        }


        [Test]
        public void Build_ShouldPopulateAllProperties()
        {
            var builder = new RequestBuilder(new StubBuilder());
            var request = builder
                .WithPath("test/method/1").WithMethod(Method.Get)
                .WithQueryString("customerId", "153").WithQueryString("orderId", "1314")
                .WithBody("<Html/>", "text/html")
                .WithHeader("X-Powered-By", "IIS")
                .WithPort(443)
                .Build();

            var expectedRequest = new RequestRegistration
            {
                Query = {Any = false, Value = "?customerId=153&orderId=1314"},
                Method = {Any = false, Value = Method.Get},
                Port = {Any = false, Value = 443},
                LocalPath = "/test/method/1",
                Body =
                {
                    Any = false,
                    Value = new Body() {Data = "<Html/>", ContentType = new ContentType() {MediaType = "text/html"}}
                },
                Headers = {Any = false, Value = new Headers(new Dictionary<string, string>() {{"X-Powered-By", "IIS"}})}
            };
            request.ShouldEqual(expectedRequest);
        }
    }
}