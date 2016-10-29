using EasyStub.Common.Request;
using EasyStub.Common.Response;
using EasyStub.Core;
using EasyStub.Test.Util;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace EasyStub.Test.Server
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
        //        .WillReturnResponse((AllRequests[]) null);

        //    Sut.FindRegisteredResponse(httpRequestMessage).Should().BeNull();
        //}

        //[Test]
        //public void FindRegisteredResponse_ShouldReturnNull_WhenNoRequestsForLocalPath1()
        //{
        //    var repoMock = Fixture.Freeze<Mock<IBehaviorRepository>>();

        //    var httpRequestMessage = Fixture.Freeze<HttpRequestMessage>();


        //    repoMock.Setup(m => m.FindByLocalPath(httpRequestMessage.RequestUri.LocalPath))
        //        .WillReturnResponse(new AllRequests[0]);

        //    Sut.FindRegisteredResponse(httpRequestMessage).Should().BeNull();
        //}

        //[Test]
        //public void FindRegisteredResponse_ShouldReturnNull_WhenMatcherDoesntFindAHit()
        //{
        //    var httpRequestMessage = Fixture.Freeze<HttpRequestMessage>();
        //    var httpRequstModel = Fixture.Freeze<HttpRequestModel>();
        //    var httpRequest =
        //        Fixture.Build<AllRequests>()
        //            .With(s => s.LocalPath, httpRequestMessage.RequestUri.LocalPath)
        //            .Freeze(Fixture);

        //    var matchingRequests = new[] {httpRequest};

        //    var behaviorRepo = Fixture.Freeze<Mock<IBehaviorRepository>>();
        //    var modelTransformer = Fixture.Freeze<Mock<IModelTransformer>>();
        //    var matcher = Fixture.Freeze<Mock<IRequestMatcher>>();
        //    modelTransformer.Setup(m => m.Transform(httpRequestMessage)).WillReturnResponse(httpRequstModel);
        //    matcher.Setup(m => m.Match(matchingRequests, httpRequstModel))
        //        .WillReturnResponse((AllRequests) null)
        //        .Verifiable();
        //    behaviorRepo.Setup(m => m.FindByLocalPath(It.IsAny<string>())).WillReturnResponse(matchingRequests);

        //    Sut.FindRegisteredResponse(httpRequestMessage).Should().BeNull();
        //    matcher.VerifyAll();
        //}

        //[Test]
        //public void FindRegisteredResponse_ShouldReturnResponseAndUnregisterRequest_WhenMatcherFindsAHit()
        //{
        //    var httpRequestMessage = Fixture.Freeze<HttpRequestMessage>();
        //    var httpRequstModel = Fixture.Freeze<HttpRequestModel>();
        //    var requestRegistration =
        //        Fixture.Build<AllRequests>()
        //            .With(s => s.LocalPath, httpRequestMessage.RequestUri.LocalPath)
        //            .Freeze(Fixture);

        //    var response = Fixture.Freeze<HttpResponseMessage>();
        //    var stubresponse = Fixture.Freeze<HttpResponseModel>();

        //    var matchingRequests = new[] {requestRegistration};

        //    var behaviorRepo = Fixture.Freeze<Mock<IBehaviorRepository>>();
        //    var modelTransformer = Fixture.Freeze<Mock<IModelTransformer>>();
        //    var matcher = Fixture.Freeze<Mock<IRequestMatcher>>();
        //    modelTransformer.Setup(m => m.Transform(httpRequestMessage)).WillReturnResponse(httpRequstModel);
        //    modelTransformer.Setup(m => m.Transform(stubresponse)).WillReturnResponse(response);

        //    matcher.Setup(m => m.Match(matchingRequests, httpRequstModel)).WillReturnResponse(matchingRequests[0]);
        //    behaviorRepo.Setup(m => m.FindByLocalPath(It.IsAny<string>())).WillReturnResponse(matchingRequests);
        //    behaviorRepo.Setup(m => m.Unregister(requestRegistration)).WillReturnResponse(stubresponse);

        //    Sut.FindRegisteredResponse(httpRequestMessage).Should().Be(response);
        //}

        //ss
        [Test]
        public void FindRegisteredResponse1_ShouldReturnNull_WhenNoRequestsForLocalPath()
        {
            var repoMock = Fixture.Freeze<Mock<IBehaviorRepository>>();
            var httpRequestMessage = Fixture.Freeze<HttpRequestModel>();


            repoMock.Setup(m => m.FindByLocalPath(httpRequestMessage.LocalPath)).Returns((RequestRegistrationModel[]) null);

            Sut.FindRegisteredResponse(httpRequestMessage).Should().BeNull();
        }

        [Test]
        public void FindRegisteredResponse1_ShouldReturnNull_WhenNoRequestsForLocalPath1()
        {
            var repoMock = Fixture.Freeze<Mock<IBehaviorRepository>>();

            var httpRequestMessage = Fixture.Freeze<HttpRequestModel>();


            repoMock.Setup(m => m.FindByLocalPath(httpRequestMessage.LocalPath)).Returns(new RequestRegistrationModel[0]);

            Sut.FindRegisteredResponse(httpRequestMessage).Should().BeNull();
        }

        [Test]
        public void FindRegisteredResponse1_ShouldReturnNull_WhenMatcherDoesntFindAHit()
        {
            var httpRequstModel = Fixture.Freeze<HttpRequestModel>();
            var httpRequest =
                Fixture.Build<RequestRegistrationModel>().With(s => s.LocalPath, httpRequstModel.LocalPath).Freeze(Fixture);

            var matchingRequests = new[] {httpRequest};

            var behaviorRepo = Fixture.Freeze<Mock<IBehaviorRepository>>();
            var matcher = Fixture.Freeze<Mock<IRequestMatcher>>();
            matcher.Setup(m => m.Match(matchingRequests, httpRequstModel))
                .Returns((RequestRegistrationModel) null)
                .Verifiable();
            behaviorRepo.Setup(m => m.FindByLocalPath(It.IsAny<string>())).Returns(matchingRequests);

            Sut.FindRegisteredResponse(httpRequstModel).Should().BeNull();
            matcher.VerifyAll();
        }

        [Test]
        public void FindRegisteredResponse1_ShouldReturnResponse_WhenMatcherFindsAHit()
        {
            var httpRequstModel = Fixture.Freeze<HttpRequestModel>();
            var requestRegistration =
                Fixture.Build<RequestRegistrationModel>().With(s => s.LocalPath, httpRequstModel.LocalPath).Freeze(Fixture);


            var httpResponseModel = Fixture.Freeze<HttpResponseModel>();

            var matchingRequests = new[] {requestRegistration};

            var behaviorRepo = Fixture.Freeze<Mock<IBehaviorRepository>>();
            var matcher = Fixture.Freeze<Mock<IRequestMatcher>>();

            matcher.Setup(m => m.Match(matchingRequests, httpRequstModel)).Returns(matchingRequests[0]);
            behaviorRepo.Setup(m => m.FindByLocalPath(It.IsAny<string>())).Returns(matchingRequests);
            behaviorRepo.Setup(m => m.Get(requestRegistration)).Returns(httpResponseModel);

            Sut.FindRegisteredResponse(httpRequstModel).Should().Be(httpResponseModel);
        }
    }
}