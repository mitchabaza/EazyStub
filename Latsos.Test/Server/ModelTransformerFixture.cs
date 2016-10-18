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
using Ploeh.AutoFixture;

namespace Latsos.Test.Server
{
    public class ModelTransformerFixture:FixtureBase<ModelTransformer>
    {
        [Test]
        public void TransformRequest_Should_Work()
        {
            var content = new StringContent(Fixture.Create<string>());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var request = Fixture.Build<HttpRequestMessage>().With(s=>s.Content, content).Create();
            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            request.Headers.Add("Forwarded", "for=192.0.2.43, for=198.51.100.17");
            var body = new Body() {Data=request.Content.ReadAsStringAsync().Result, Type = request.Content.Headers.ContentType.MediaType};
            var method = request.Method;
            var query = request.RequestUri.Query;
            var port = request.RequestUri.Port;
            var headers = new Headers();
            request.Headers.ForEach(h => headers.Add(h.Key, string.Join(",", h.Value)));
            string localPath = request.RequestUri.LocalPath;
            var httpRequestModel = new HttpRequestModel(body,method,headers,query,localPath, port);

            Sut.Transform(request).ShouldBeEquivalentTo(httpRequestModel);
        }

        [Test]
        public void TransformResponse_Should_Work()
        {
            var content = new StringContent(Fixture.Create<string>(), Encoding.Default,"application/xml");
         
            var httpResponseModel = Fixture.Build<HttpResponseModel>().With(m=>m.ContentType, "application/xml") .Create();
            httpResponseModel.Headers.Add("Accept-Encoding", "gzip, deflate");
            httpResponseModel.Headers.Add("Forwarded", "for=192.0.2.43, for=198.51.100.17");
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = httpResponseModel.StatusCode
            };
            httpResponseModel.Headers.ForEach(s=>httpResponseMessage.Headers.Add(s.Key,s.Value));
            httpResponseMessage.Content = content;
            Sut.Transform(httpResponseModel).ShouldBeEquivalentTo(httpResponseMessage);
        }
    }
}