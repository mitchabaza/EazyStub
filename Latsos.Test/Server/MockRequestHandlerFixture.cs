using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using FluentAssertions;
using Latsos.Core;
using Latsos.Web;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Latsos.Test.Server
{
    [TestFixture]
    public class MockRequestHandlerFixture : FixtureBase<MockRequestHandler>
    {
        [Test]
        public void SendAsync_ShouldNotCallRouteMatcher_WhenRouteIsReal()
        {
            HttpConfiguration configuration = Fixture.Freeze<HttpConfiguration>();

        }

        [Test]
        public void SendAsync_ShouldDelegateToBase_WhenNoRouteMatched()
        {
            
          
            var baseWasCalled = false;
            var matcher = Fixture.Freeze<Mock<IRequestEvaluator>>();
            matcher.Setup(m => m.FindRegisteredResponse(It.IsAny<HttpRequestMessage>()))
                .Returns((HttpResponseMessage) null);
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
            var response = Fixture.Create<HttpResponseMessage>();
            var matcher = Fixture.Freeze<Mock<IRequestEvaluator>>();
            matcher.Setup(m => m.FindRegisteredResponse(It.IsAny<HttpRequestMessage>())).Returns(response);
            var testHandler = new InnerHandler();

            Sut.InnerHandler = testHandler;
            
            var client = new HttpClient(Sut);
            var result=client.SendAsync(Fixture.Create<HttpRequestMessage>(), new CancellationToken()).Result;
            result.Should().Be(response);
        }
    }
}