using System.Net.Http;
using FluentAssertions;
using Latsos.Core;
using Latsos.Shared;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Latsos.Test.Server
{
    public class RequestEvaluatorFixture : FixtureBase<RequestEvaluator>
    {
        protected override void OnFixtureSetup()
        {
            var configurationMock = Fixture.Freeze<Mock<IHostingEnvironment>>();
            configurationMock.SetupGet(m => m.ApplicationVirtualPath).Returns("buzz");
        }


        //[Test]
        //public void FindRegisteredResponse_ShouldReturnNull_WhenNoRequestsForLocalPath()
        //{
        //    var repoMock = Fixture.Freeze<Mock<IBehaviorRepository>>();
        //    var httpRequestMessage = Fixture.Freeze<HttpRequestMessage>();


        //    repoMock.Setup(m => m.FindByLocalPath(httpRequestMessage.RequestUri.LocalPath))
        //        .Returns((Request[]) null);

        //    Sut.FindRegisteredResponse(httpRequestMessage).Should().BeNull();
        //}

        //[Test]
        //public void FindRegisteredResponse_ShouldReturnNull_WhenNoRequestsForLocalPath1()
        //{
        //    var repoMock = Fixture.Freeze<Mock<IBehaviorRepository>>();

        //    var httpRequestMessage = Fixture.Freeze<HttpRequestMessage>();


        //    repoMock.Setup(m => m.FindByLocalPath(httpRequestMessage.RequestUri.LocalPath))
        //        .Returns(new Request[0]);

        //    Sut.FindRegisteredResponse(httpRequestMessage).Should().BeNull();
        //}

        //[Test]
        //public void FindRegisteredResponse_ShouldReturnNull_WhenMatcherDoesntFindAHit()
        //{
        //    var httpRequestMessage = Fixture.Freeze<HttpRequestMessage>();
        //    var httpRequstModel = Fixture.Freeze<HttpRequestModel>();
        //    var httpRequest =
        //        Fixture.Build<Request>()
        //            .With(s => s.LocalPath, httpRequestMessage.RequestUri.LocalPath)
        //            .Freeze(Fixture);

        //    var matchingRequests = new[] {httpRequest};

        //    var behaviorRepo = Fixture.Freeze<Mock<IBehaviorRepository>>();
        //    var modelTransformer = Fixture.Freeze<Mock<IModelTransformer>>();
        //    var matcher = Fixture.Freeze<Mock<IRequestMatcher>>();
        //    modelTransformer.Setup(m => m.Transform(httpRequestMessage)).Returns(httpRequstModel);
        //    matcher.Setup(m => m.Match(matchingRequests, httpRequstModel))
        //        .Returns((Request) null)
        //        .Verifiable();
        //    behaviorRepo.Setup(m => m.FindByLocalPath(It.IsAny<string>())).Returns(matchingRequests);

        //    Sut.FindRegisteredResponse(httpRequestMessage).Should().BeNull();
        //    matcher.VerifyAll();
        //}

        //[Test]
        //public void FindRegisteredResponse_ShouldReturnResponseAndUnregisterRequest_WhenMatcherFindsAHit()
        //{
        //    var httpRequestMessage = Fixture.Freeze<HttpRequestMessage>();
        //    var httpRequstModel = Fixture.Freeze<HttpRequestModel>();
        //    var requestRegistration =
        //        Fixture.Build<Request>()
        //            .With(s => s.LocalPath, httpRequestMessage.RequestUri.LocalPath)
        //            .Freeze(Fixture);

        //    var response = Fixture.Freeze<HttpResponseMessage>();
        //    var stubresponse = Fixture.Freeze<HttpResponseModel>();

        //    var matchingRequests = new[] {requestRegistration};

        //    var behaviorRepo = Fixture.Freeze<Mock<IBehaviorRepository>>();
        //    var modelTransformer = Fixture.Freeze<Mock<IModelTransformer>>();
        //    var matcher = Fixture.Freeze<Mock<IRequestMatcher>>();
        //    modelTransformer.Setup(m => m.Transform(httpRequestMessage)).Returns(httpRequstModel);
        //    modelTransformer.Setup(m => m.Transform(stubresponse)).Returns(response);

        //    matcher.Setup(m => m.Match(matchingRequests, httpRequstModel)).Returns(matchingRequests[0]);
        //    behaviorRepo.Setup(m => m.FindByLocalPath(It.IsAny<string>())).Returns(matchingRequests);
        //    behaviorRepo.Setup(m => m.Unregister(requestRegistration)).Returns(stubresponse);

        //    Sut.FindRegisteredResponse(httpRequestMessage).Should().Be(response);
        //}

        //ss
        [Test]
        public void FindRegisteredResponse1_ShouldReturnNull_WhenNoRequestsForLocalPath()
        {
            var repoMock = Fixture.Freeze<Mock<IBehaviorRepository>>();
            var httpRequestMessage = Fixture.Freeze<HttpRequestModel>();


            repoMock.Setup(m => m.FindByLocalPath(httpRequestMessage.LocalPath)).Returns((RequestRegistration[]) null);

            Sut.FindRegisteredResponse(httpRequestMessage).Should().BeNull();
        }

        [Test]
        public void FindRegisteredResponse1_ShouldReturnNull_WhenNoRequestsForLocalPath1()
        {
            var repoMock = Fixture.Freeze<Mock<IBehaviorRepository>>();

            var httpRequestMessage = Fixture.Freeze<HttpRequestModel>();


            repoMock.Setup(m => m.FindByLocalPath(httpRequestMessage.LocalPath)).Returns(new RequestRegistration[0]);

            Sut.FindRegisteredResponse(httpRequestMessage).Should().BeNull();
        }

        [Test]
        public void FindRegisteredResponse1_ShouldReturnNull_WhenMatcherDoesntFindAHit()
        {
            var httpRequstModel = Fixture.Freeze<HttpRequestModel>();
            var httpRequest =
                Fixture.Build<RequestRegistration>().With(s => s.LocalPath, httpRequstModel.LocalPath).Freeze(Fixture);

            var matchingRequests = new[] {httpRequest};

            var behaviorRepo = Fixture.Freeze<Mock<IBehaviorRepository>>();
            var matcher = Fixture.Freeze<Mock<IRequestMatcher>>();
            matcher.Setup(m => m.Match(matchingRequests, httpRequstModel))
                .Returns((RequestRegistration) null)
                .Verifiable();
            behaviorRepo.Setup(m => m.FindByLocalPath(It.IsAny<string>())).Returns(matchingRequests);

            Sut.FindRegisteredResponse(httpRequstModel).Should().BeNull();
            matcher.VerifyAll();
        }

        [Test]
        public void FindRegisteredResponse1_ShouldReturnResponseAndUnregisterRequest_WhenMatcherFindsAHit()
        {
            var httpRequstModel = Fixture.Freeze<HttpRequestModel>();
            var requestRegistration =
                Fixture.Build<RequestRegistration>().With(s => s.LocalPath, httpRequstModel.LocalPath).Freeze(Fixture);


            var httpResponseModel = Fixture.Freeze<HttpResponseModel>();

            var matchingRequests = new[] {requestRegistration};

            var behaviorRepo = Fixture.Freeze<Mock<IBehaviorRepository>>();
            var matcher = Fixture.Freeze<Mock<IRequestMatcher>>();

            matcher.Setup(m => m.Match(matchingRequests, httpRequstModel)).Returns(matchingRequests[0]);
            behaviorRepo.Setup(m => m.FindByLocalPath(It.IsAny<string>())).Returns(matchingRequests);
            behaviorRepo.Setup(m => m.Unregister(requestRegistration)).Returns(httpResponseModel);

            Sut.FindRegisteredResponse(httpRequstModel).Should().Be(httpResponseModel);
        }
    }
}