using System.Net.Http;
using FluentAssertions;
using Latsos.Core;
using Latsos.Shared;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Latsos.Test.Server
{
    public class RequestMatcherFixture : FixtureBase<RequestEvaluator>
    {
        protected override void OnFixtureSetup()
        {
            var configurationMock = Fixture.Freeze<Mock<IHostingEnvironment>>();
            configurationMock.SetupGet(m => m.ApplicationVirtualPath).Returns("buzz");
        }

        //[Test]
        //public void FindRegisteredResponse_ShouldReturnResponse_WhenRequestMatches()
        //{
        //    var repoMock = Fixture.Freeze<Stub<IBehaviorRepository>>();
        //    var xformerMock = Fixture.Freeze<Stub<IModelTransformer>>();

        //    var httpRequestMessage = Fixture.Freeze<HttpRequestMessage>();
        //    var httpRequest = Fixture.Freeze<HttpRequestRegistration>();
        //    var httpResponseMessage = Fixture.Freeze<HttpResponseMessage>();
        //    var httpResponse = Fixture.Freeze<StubHttpResponse>();

        //    xformerMock.Setup(m => m.Transform(httpRequestMessage)).Returns(httpRequest);
        //    xformerMock.Setup(m => m.Transform(httpResponse)).Returns(httpResponseMessage);

        //    repoMock.Setup(m => m.Find(httpRequest)).Returns(httpResponse);
        //    Sut.FindRegisteredResponse(httpRequestMessage).Should().Be (httpResponseMessage);

        //}

        [Test]
        public void FindRegisteredResponse_ShouldReturnNull_WhenNoRequestsForLocalPath()
        {
            var repoMock = Fixture.Freeze<Mock<IBehaviorRepository>>();
              var httpRequestMessage = Fixture.Freeze<HttpRequestMessage>();
            
           
            repoMock.Setup(m => m.FindByLocalPath(httpRequestMessage.RequestUri.LocalPath)).Returns( (HttpRequestRegistration[]) null);

            Sut.FindRegisteredResponse(httpRequestMessage).Should().BeNull();
        }
        [Test]
        public void FindRegisteredResponse_ShouldReturnNull_WhenNoRequestsForLocalPath1()
        {
            var repoMock = Fixture.Freeze<Mock<IBehaviorRepository>>();
            
            var httpRequestMessage = Fixture.Freeze<HttpRequestMessage>();


            repoMock.Setup(m => m.FindByLocalPath(httpRequestMessage.RequestUri.LocalPath)).Returns(new HttpRequestRegistration[0]);

            Sut.FindRegisteredResponse(httpRequestMessage).Should().BeNull();
        }
        [Test]
        public void FindRegisteredResponse_ShouldCallMatcher_WhenRepositoryReturnsMultipleHits()
        {
           
            var repoMock = Fixture.Freeze<Mock<IBehaviorRepository>>();
            var xformerMock = Fixture.Freeze<Mock<IModelTransformer>>();

            var httpRequestMessage = Fixture.Freeze<HttpRequestMessage>();
            var httpRequest = Fixture.Build<HttpRequestRegistration>().With(s=>s.LocalPath,httpRequestMessage.RequestUri.LocalPath).Create();
            Fixture.Inject(httpRequest);
            
            var httpResponseMessage = Fixture.Freeze<HttpResponseMessage>();
            var httpResponse = Fixture.Freeze<StubHttpResponse>();

            xformerMock.Setup(m => m.Transform(httpResponse)).Returns(httpResponseMessage);
             
            repoMock.Setup(m => m.FindByLocalPath(It.IsAny<string>())).Returns(new[] {httpRequest});

            repoMock.Setup(m => m.Remove(httpRequest)).Returns(httpResponse);

            Sut.FindRegisteredResponse(httpRequestMessage).Should().Be(httpResponseMessage);
        }
       
    }
}