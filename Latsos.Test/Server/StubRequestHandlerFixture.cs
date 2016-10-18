using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using FluentAssertions;
using Latsos.Core;
using Latsos.Shared;
using Latsos.Web;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Latsos.Test.Server
{
    [TestFixture]
    public class StubRequestHandlerFixture : FixtureBase<StubRequestHandler>
    {
        [Test]
        public void SendAsync_ShouldNotCallRouteMatcher_WhenRouteMatchesRealRoute()
        {
            HttpConfiguration configuration = Fixture.Freeze<HttpConfiguration>();

        }

        [Test]
        public void SendAsync_ShouldDelegateToBase_WhenNoRouteMatched()
        {
            
          
            var baseWasCalled = false;
            var matcher = Fixture.Freeze<Mock<IRequestEvaluator>>();
            matcher.Setup(m => m.FindRegisteredResponse(It.IsAny<HttpRequestModel>())).Returns((HttpResponseModel) null);
            var transformer = Fixture.Freeze<Mock<IModelTransformer>>();
            transformer.Setup(m => m.Transform(It.IsAny<HttpRequestMessage>())).Returns(Fixture.Create<HttpRequestModel>());

            var testHandler = new InnerHandler();

            Sut.InnerHandler = testHandler;

            testHandler.SetHandler((m, c) =>
            {
                baseWasCalled = true;
                var task = new TaskCompletionSource<HttpResponseMessage>();

                task.SetResult(new HttpResponseMessage());

                return task.Task;
            }
                );

            var client = new HttpClient(Sut);
            client.SendAsync(Fixture.Create<HttpRequestMessage>(), new CancellationToken());
            baseWasCalled.Should().BeTrue();
        }

        [Test]
        public void SendAsync_ShouldReturnMockResponse_WhenRouteMatched()
        {
            var responseModel = Fixture.Create<HttpResponseModel>();
            var response = Fixture.Create<HttpResponseMessage>();
               
            var requestModel = Fixture.Create<HttpRequestModel>();
            var matcher = Fixture.Freeze<Mock<IRequestEvaluator>>();
            var transformer=Fixture.Freeze<Mock<IModelTransformer>>();
            transformer.Setup(m => m.Transform(It.IsAny<HttpRequestMessage>())).Returns(requestModel);
            transformer.Setup(m => m.Transform(responseModel)).Returns(response);
            matcher.Setup(m => m.FindRegisteredResponse(It.IsAny<HttpRequestModel>())).Returns(responseModel);

            
            var result= GetHttpClient().SendAsync(Fixture.Create<HttpRequestMessage>(), new CancellationToken()).Result;
            result.Should().Be(response);
        }

        private HttpClient GetHttpClient()
        {
            var testHandler = new InnerHandler();

            Sut.InnerHandler = testHandler;

            return new HttpClient(Sut);
        }
    }
}