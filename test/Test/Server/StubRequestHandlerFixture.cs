using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using EasyStub.Common.Request;
using EasyStub.Common.Response;
using EasyStub.Core;
using EasyStub.Server;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace EasyStub.Test.Server
{
    [TestFixture]
    public class StubRequestHandlerFixture : FixtureBase<StubRequestHandler>
    {
        private HttpRequestMessage _request;

        private Mock<HttpRouteCollection> _routes;

        protected override void OnFixtureSetup()
        {
            base.OnFixtureSetup();
            var httpRequestContext = new HttpRequestContext();
            _routes = Fixture.Freeze<Mock<HttpRouteCollection>>();
            httpRequestContext.Configuration = new HttpConfiguration(_routes.Object);
            _request = Fixture.Freeze<HttpRequestMessage>();
            _request.Properties.Add(HttpPropertyKeys.RequestContextKey, httpRequestContext);
        }

        [Test]
        public void SendAsync_ShouldDelegateToBase_WhenRouteIsReal()
        {
            var routeData = new HttpRouteData(new HttpRoute());
            routeData.Values.Add("MS_SubRoutes",
                new[] {new HttpRouteData(new HttpRoute("mom", new HttpRouteValueDictionary()))});
            var evaluator = Fixture.Freeze<Mock<IRequestEvaluator>>();
            _routes.Setup(r => r.GetRouteData(It.IsAny<HttpRequestMessage>())).Returns(routeData);
            bool baseWasCalled = false;
            GetHttpClient((m,c)=> { baseWasCalled = true; return  new TaskCompletionSource<HttpResponseMessage>().Task;}).SendAsync(_request, new CancellationToken());
            baseWasCalled.Should().BeTrue();
            evaluator.Verify(s => s.FindRegisteredResponse(It.IsAny<HttpRequestModel>()), Times.Never);
        }

        [Test]
        public void SendAsync_ShouldDelegateToBase_WhenNoRouteMatched()
        {
            var baseWasCalled = false;
            var matcher = Fixture.Freeze<Mock<IRequestEvaluator>>();
            matcher.Setup(m => m.FindRegisteredResponse(It.IsAny<HttpRequestModel>())).Returns((HttpResponseModel) null);
            var transformer = Fixture.Freeze<Mock<IModelTransformer>>();
            transformer.Setup(m => m.Transform(It.IsAny<HttpRequestMessage>()))
                .Returns(Fixture.Create<HttpRequestModel>());

            GetHttpClient((m, c) =>
            {
                baseWasCalled = true;
                var task = new TaskCompletionSource<HttpResponseMessage>();

                task.SetResult(new HttpResponseMessage());

                return task.Task;
            }
                ).SendAsync(_request, new CancellationToken());
            baseWasCalled.Should().BeTrue();
        }


        [Test]
        public void SendAsync_ShouldReturStubResponse_WhenRouteMatched()
        {
            var responseModel = Fixture.Create<HttpResponseModel>();
            var response = Fixture.Create<HttpResponseMessage>();

            var requestModel = Fixture.Create<HttpRequestModel>();
            var matcher = Fixture.Freeze<Mock<IRequestEvaluator>>();
            var transformer = Fixture.Freeze<Mock<IModelTransformer>>();
            transformer.Setup(m => m.Transform(It.IsAny<HttpRequestMessage>())).Returns(requestModel);
            transformer.Setup(m => m.Transform(responseModel)).Returns(response);
            matcher.Setup(m => m.FindRegisteredResponse(It.IsAny<HttpRequestModel>())).Returns(responseModel);

            var result = GetHttpClient().SendAsync(_request, new CancellationToken()).Result;
            result.Should().Be(response);
        }

        private HttpClient GetHttpClient(Func<HttpRequestMessage,
            CancellationToken, Task<HttpResponseMessage>> func = null)
        {
            var testHandler = new InnerHandlerForTesting();

            Sut.InnerHandler = testHandler;

            if (func != null)
            {
                testHandler.SetHandler(func);
            }
            else
            {
                testHandler.SetHandler((m, c) => new TaskCompletionSource<HttpResponseMessage>().Task);
            }
            return new HttpClient(Sut);
        }
    }
}