using System.Net.Http;
using FluentAssertions;
using Latsos.Core;
using Latsos.Shared;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Latsos.Test.Server
{
    public class LocalPathProcessorFixture : FixtureBase<LocalPathProcessor>
    {

        [Test]
        public void Execute_ShouldReplaceVirtualPathInFirstSegment()
        {

            Fixture.Freeze<Mock<IHostingEnvironment>>().SetupGet(m => m.ApplicationVirtualPath).Returns("/base");
            var model = Fixture.Build<HttpRequestModel>().With(m => m.LocalPath, "/base/customer/1/base").Create();
            Sut.Execute(model);
            model.LocalPath.Should().Be("/customer/1/base");
        }
    }
}