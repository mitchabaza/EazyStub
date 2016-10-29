using EasyStub.Common;
using EasyStub.Common.Request;
using EasyStub.Core;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace EasyStub.Test.Server
{
    public class LocalPathProcessorFixture : FixtureBase<LocalPathProcessor>
    {
        [Test]
        public void Execute_ShouldReplaceVirtualPathInFirstSegment()
        {
            Fixture.Freeze<Mock<IHostingEnvironment>>().SetupGet(m => m.ApplicationVirtualPath).Returns("/base");
            var model = new HttpRequestModel(Fixture.Create<Body>(), Fixture.Create<Method>(), Fixture.Create<Headers>(),
                Fixture.Create<string>(), "/base/customer/1/base", 443);
            Sut.Execute(model).LocalPath.Should().Be("/customer/1/base");
        }
    }
}