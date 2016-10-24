using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using FluentAssertions;
using Latsos.Core;
using Latsos.Shared;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Ploeh.AutoFixture;
using Encoding = System.Text.Encoding;

namespace Latsos.Test.Server
{
    public class ModelTransformerFixture : FixtureBase<ModelTransformer>
    {
        [Test]
        public void TransformRequest_ShouldInvokePostProcessorBeforeReturning()

        {
            var request = Fixture.Build<HttpRequestMessage>()
                .Create();
            Fixture.Freeze<Mock<IRequestModelProcessor>>()
                .Setup(s => s.Execute(It.IsAny<HttpRequestModel>()))
                .Callback<HttpRequestModel>(r => r.LocalPath = "hi mom");
            Sut.Transform(request
                ).LocalPath.Should().Be("hi mom");
        }


        [Test]
        public void TransformResponse_ShouldNotSupplyCharSetMediatType_WhenBothMissing()
        {
            var response = Fixture.Build<HttpResponseModel>().Without(s=>s.Body).Create();
            response.Body = new Body() {Data = "hi mom!"};
            var exepcted = new StringContent("hi mom");
            Sut.Transform(response).Content.ShouldBeEquivalentTo(exepcted);
        }
        [Test]
        public void TransformResponse_ShouldNotSupplyCharSetMediaType_WhenEitherMissing()
        {
            var response = Fixture.Build<HttpResponseModel>().Without(s => s.Body).Create();
            response.Body = new Body() { Data = "hi mom!" ,ContentType = new ContentType() {CharSet = Encoding.Default.WebName } };
            var exepcted = new StringContent("hi mom");
            Sut.Transform(response).Content.ShouldBeEquivalentTo(exepcted);
        }

        [Test]
        public void TransformRequest_ShouldReturnAllAttributes()
        {
            var request = Fixture.Create<HttpRequestMessage>();
            var httpRequestModel = TransformModel(request);
            Sut.Transform(request).ShouldBeEquivalentTo(httpRequestModel);
        }

        private HttpRequestModel TransformModel(HttpRequestMessage request)
        {
            var content = new StringContent(Fixture.Create<string>());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = content;
            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            request.Headers.Add("Forwarded", "for=192.0.2.43, for=198.51.100.17");

            var body = new Body()
            {
                Data = request.Content.ReadAsStringAsync().Result,
                ContentType = new ContentType()
                {
                    MediaType = content.Headers.ContentType.MediaType,
                    CharSet =
                        content.Headers?.ContentType.CharSet != null
                            ? Encoding.GetEncoding(content.Headers?.ContentType?.CharSet).WebName
                            : null
                }
            };
            var method = request.Method;
            var query = request.RequestUri.Query;
            var port = request.RequestUri.Port;
            var headers = new Headers();
            request.Headers.ForEach(h => headers.Add(h.Key, string.Join(",", h.Value)));
            string localPath = request.RequestUri.LocalPath;
            return new HttpRequestModel(body, method, headers, query, localPath, port);
        }

        [Test]
        public void TransformResponse_ShouldReturnCorrectResponse_WhenAllAttributesPresent()
        {
            var httpResponseModel =
                Fixture.Build<HttpResponseModel>()
                    .With(m => m.Body,
                        new Body()
                        {
                            Data = "",
                            ContentType = new ContentType() {MediaType = "application/xml", CharSet = Encoding.Default.WebName }
                        })
                    .Create();
            httpResponseModel.Headers.Add("Accept-Encoding", "gzip, deflate");
            httpResponseModel.Headers.Add("Forwarded", "for=192.0.2.43, for=198.51.100.17");

            var httpResponseMessage = GetExpectedResponse(httpResponseModel);

            Sut.Transform(httpResponseModel).ShouldBeEquivalentTo(httpResponseMessage);
        }

        private object GetExpectedResponse(HttpResponseModel httpResponseModel)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = httpResponseModel.StatusCode
            };
            httpResponseModel.Headers.Dictionary.ForEach(s => httpResponseMessage.Headers.Add(s.Key, s.Value.ToString()));
            var content = new StringContent(httpResponseModel.Body.Data, Encoding.GetEncoding(httpResponseModel.Body.ContentType.CharSet) , httpResponseModel.Body.ContentType.MediaType);
            httpResponseMessage.Content = content;
            return httpResponseMessage;
        }

        [Test]
        public void TransformResponse_ShouldSetCorrectCharset_WhenPresent()
        {
            var content = new StringContent(Fixture.Create<string>(), Encoding.UTF8, "application/xml");

            var httpResponseModel =
                Fixture.Build<HttpResponseModel>()
                    .With(m => m.Body,
                        new Body()
                        {
                            Data = "Your mom",
                            ContentType = new ContentType() {MediaType = "application/xml", CharSet = "utf-8"}
                        })
                    .Create();
            httpResponseModel.Headers.Add("Accept-Encoding", "gzip, deflate");
            httpResponseModel.Headers.Add("Forwarded", "for=192.0.2.43, for=198.51.100.17");
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            httpResponseMessage.StatusCode = httpResponseModel.StatusCode;
            httpResponseModel.Headers.Dictionary.ForEach(s => httpResponseMessage.Headers.Add(s.Key, s.Value.ToString()));
            httpResponseMessage.Content = content;
            Sut.Transform(httpResponseModel).ShouldBeEquivalentTo(httpResponseMessage);
        }

        [Test]
        public void TransformResponse_ShouldReturnCorrectResponse_WhenSomeAttributesPresent()
        {
            var httpResponseModel = Fixture.Build<HttpResponseModel>().With(m => m.Body, new Body()).Create();
            httpResponseModel.Headers.Add("Accept-Encoding", "gzip, deflate");
            httpResponseModel.Headers.Add("Forwarded", "for=192.0.2.43, for=198.51.100.17");
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            httpResponseMessage.StatusCode = httpResponseModel.StatusCode;
            httpResponseModel.Headers.Dictionary.ForEach(s => httpResponseMessage.Headers.Add(s.Key, s.Value.ToString()));
            Sut.Transform(httpResponseModel).ShouldBeEquivalentTo(httpResponseMessage);
        }
    }
}