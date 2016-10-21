using System;
using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using Latsos.Client;
using Latsos.Shared;
using NUnit.Framework;

namespace Latsos.Test
{
    public class RequestBuilderFixture
    {
        [Test]
        public void RequestBuilder_ShouldCreateRequest_WhenQueryStringSpecified()
        {
            var builder = new StubBuilder();

            var id = Guid.NewGuid().ToString();
            var registration = builder.Request()
                .QueryString("id", id)
                .Path("buzz/dance")
                .Build();

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
            var registration = builder.Request()
                .QueryString("id", id).QueryString("jack", "jill")
                .Path("etc/1").Method(Method.Post)
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
            var registration = builder.Request()
                .QueryString("tranId", id).QueryString("orgId", "64556456456").QueryString("custid", "654654")
                .Path("customer/delete/645564").Method(Method.Post)
                .Port(9999)
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


        [Test] public void Build_ShouldResetInternalState()
        {
            var builder = new RequestBuilder(new StubBuilder());
            var request = builder
                .Method(Method.Get)
                .Path("test/method/1")
                .QueryString("customerId", "153").QueryString("orderId", "1314")
                .Body("<Html/>", "text/html")
                .Header("X-Powered-By", "IIS")
                .Port(443)
                .Build();

            var request2 = builder.Build();

            request2.ShouldEqual(new RequestRegistration());
        }


        [Test]
        public void Build_ShouldPopulateAllProperties()
        {
            var builder = new RequestBuilder(new StubBuilder());
            var request = builder
                .Method(Method.Get)
                .Path("test/method/1")
                .QueryString("customerId", "153").QueryString("orderId","1314")
                .Body("<Html/>", "text/html")
                .Header("X-Powered-By", "IIS")
                .Port(443)
                .Build();

            var expectedRequest = new RequestRegistration
            {
                Query = {Any=false, Value = "?customerId=153&orderId=1314" },
                Method = { Any = false, Value = Method.Get},
                Port = { Any = false, Value = 443},
                LocalPath = "/test/method/1",
                Body =
                {
                    Any=false,Value = new Body() {Data = "<Html/>", ContentType = new ContentType() {MediaType = "text/html"}}
                },
                Headers = { Any = false, Value = new Headers(new Dictionary<string, string>() {{"X-Powered-By", "IIS"}})}
            };
            request.ShouldEqual(expectedRequest);

        }


    }
}