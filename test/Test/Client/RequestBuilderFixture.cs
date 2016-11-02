using System;
using System.Collections.Generic;
using EasyStub.Client;
using EasyStub.Common;
using EasyStub.Common.Request;
using EasyStub.Test.Util;
using FluentAssertions;
using NUnit.Framework;

namespace EasyStub.Test.Client
{
    public class RequestBuilderFixture
    {
        private RequestBuilder _requestBuilder;

        [SetUp]
        public void SetUp()
        {
            _requestBuilder = new RequestBuilder(new StubBuilder());

        }
        [Test]
        public void RequestBuilder_ShouldCreateRequest_WhenQueryStringSpecified()
        {
         
            var id = Guid.NewGuid().ToString();
            var registration = _requestBuilder
                .WithPath("buzz/dance")
                .AndQueryString("id", id).Build();

            registration.ShouldBeEquivalentTo(
                new RequestRegistrationModel()
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
           
            var id = Guid.NewGuid().ToString();
            var registration = _requestBuilder
                .WithPath("etc/1").AndMethod(Method.Post).AndQueryString("id", id).AndQueryString("jack", "jill")
                .Build();

            registration.ShouldBeEquivalentTo(
                new RequestRegistrationModel()
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
        
            var id = Guid.NewGuid().ToString();
            var registration = _requestBuilder
                .WithPath("customer/delete/645564")
                .AndMethod(Method.Post)
                .AndQueryString("tranId", id)
                .AndQueryString("orgId", "64556456456")
                .AndQueryString("custid", "654654")
                .AndPort(9999)
                .Build();

            registration.ShouldBeEquivalentTo(
                new RequestRegistrationModel()
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
            _requestBuilder
                  .WithPath("test/method/1")
                .AndMethod(Method.Get).AndQueryString("customerId", "153").AndQueryString("orderId", "1314")
                .AndBody("<Html/>", "text/html")
                .AndHeader("X-Powered-By", "IIS")
                .AndPort(443)
                .Build();

            var request2 = _requestBuilder.WithPath("test/method/1").Build();

            request2.ShouldEqual(new RequestRegistrationModel() {LocalPath = "test/method/1" });
        }


        [Test]
        public void Build_ShouldPopulateAllProperties()
        {
            var builder = new RequestBuilder(new StubBuilder());
            var request = builder
                .WithPath("test/method/1").AndMethod(Method.Get)
                .AndQueryString("customerId", "153").AndQueryString("orderId", "1314")
                .AndBody("<Html/>", "text/html")
                .AndHeader("X-Powered-By", "IIS")
                .AndPort(443)
                .Build();

            var expectedRequest = new RequestRegistrationModel
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
                Headers = {Any = false, Value = new Headers(new Dictionary<string, string>() {{"x-powered-by", "IIS"}})}
            };
            request.ShouldEqual(expectedRequest);
        }
    }
}