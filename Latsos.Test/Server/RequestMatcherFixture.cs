using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Latsos.Core;
using Latsos.Shared;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Latsos.Test.Server
{
    public class RequestMatcherFixture : FixtureBase<RequestMatcher>
    {
        [Test]
        public void Match_ShouldReturnNull_WhenAllAttributesRequireExactMatchAndThereAreNone()
        {
            var model = Fixture.Create<HttpRequestModel>();
            var requestRegistration = Fixture.Create<RequestRegistration>();
            requestRegistration.Port.Any = false;
            requestRegistration.Method.Any = false;
            requestRegistration.Body.Any = false;
            requestRegistration.Headers.Any = false;
            requestRegistration.Query.Any = false;

            Sut.Match(new[] {requestRegistration}, model).Should().BeNull();
        }

        [Test]
        public void Match_ShouldReturnMatchingRequest_WhenAllAttributesAreAnyAndPathMatches()
        {
            var model = Fixture.Create<HttpRequestModel>();
            var requestRegistration = new RequestRegistration {LocalPath = model.LocalPath};

            Sut.Match(new[] {requestRegistration}, model).Should().Be(requestRegistration);
        }
        [Test]
        public void Match_ShouldReturnNull_WhenAllAttributesAreAnyButPathDoesntMatch()
        {
            var model = Fixture.Create<HttpRequestModel>();
            var requestRegistration = new RequestRegistration { LocalPath = model.LocalPath + "1" };

            Sut.Match(new[] { requestRegistration }, model).Should().Be(null);
        }
        [Test]
        public void Match_ShouldReturnMatchingRequest_WhenMethodIsAnyAndAllOtherAttributesMatchExactly()
        {
            var model = Fixture.Create<HttpRequestModel>();
            var requestRegistration = new RequestRegistration
            {
                LocalPath = model.LocalPath,
                Method = {Any =true,Value = null},
                Headers = { Any = false, Value = model.Headers},
                Body = {Any=false,Value = model.Body},
                Port = { Any = false, Value = model.Port},
                Query = { Any = false, Value = model.Query}
            };
            Sut.Match(new[] { requestRegistration }, model).Should().Be(requestRegistration);
        }

        [Test]
        public void Match_ShouldReturnMatchingRequest_WhenPortIsAnyAndAllOtherAttributesMatchExactly()
        {
            var model = Fixture.Create<HttpRequestModel>();
            var requestRegistration = new RequestRegistration
            {
                LocalPath = model.LocalPath,
                Port = { Any = true, Value = 0 },
                Headers = { Any = false, Value = model.Headers },
                Body = { Any = false, Value = model.Body },
                Method = { Any = false, Value = model.Method },
                Query = { Any = false, Value = model.Query }
            };
            Sut.Match(new[] { requestRegistration }, model).Should().Be(requestRegistration);
        }
        [Test]
        public void Match_ShouldReturnMatchingRequest_WhenHeadersIsAnyAndAllOtherAttributesMatchExactly()
        {
            var model = Fixture.Create<HttpRequestModel>();
            var requestRegistration = new RequestRegistration
            {
                LocalPath = model.LocalPath,
                Headers = { Any = true, Value = null },
                Port = { Any = false, Value = model.Port },
                Body = { Any = false, Value = model.Body },
                Method = { Any = false, Value = model.Method },
                Query = { Any = false, Value = model.Query }
            };
            Sut.Match(new[] { requestRegistration }, model).Should().Be(requestRegistration);
        }

        [Test]
        public void Match_ShouldReturnMatchingRequest_WhenBodyIsAnyAndAllOtherAttributesMatchExactly()
        {
            var model = Fixture.Create<HttpRequestModel>();
            var requestRegistration = new RequestRegistration
            {
                LocalPath = model.LocalPath,
                Body = { Any = true, Value = null },
                Port = { Any = false, Value = model.Port },
                Headers = { Any = false, Value = model.Headers },
                Method = { Any = false, Value = model.Method },
                Query = { Any = false, Value = model.Query }
            };
            Sut.Match(new[] { requestRegistration }, model).Should().Be(requestRegistration);
        }

        [Test]
        public void Match_ShouldReturnMatchingRequest_WhenQueryIsAnyAndAllOtherAttributesMatchExactly()
        {
            var model = Fixture.Create<HttpRequestModel>();
            var requestRegistration = new RequestRegistration
            {
                LocalPath = model.LocalPath,
                Query = { Any = true, Value = null },
                Port = { Any = false, Value = model.Port },
                Headers = { Any = false, Value = model.Headers },
                Method = { Any = false, Value = model.Method },
                Body = { Any = false, Value = model.Body }
            };
            Sut.Match(new[] { requestRegistration }, model).Should().Be(requestRegistration);
        }
    }
}