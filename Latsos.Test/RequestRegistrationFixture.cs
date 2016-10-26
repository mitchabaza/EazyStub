using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Http;
using System.Text;
using FluentAssertions;
using Latsos.Client;
using Latsos.Shared;
using Latsos.Shared.Request;
using Latsos.Test.Util;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Latsos.Test
{
    [TestFixture]
    public class RequestRegistrationFixture
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void GetHashCode_ShouldBeSame_WhenObjectsSame()
        {
            var httpRequest3 = _fixture.Create<RequestRegistration>();
            var httpRequest4 = httpRequest3.Copy();


            httpRequest3.GetHashCode().Should().Be(httpRequest4.GetHashCode());

        }

     
        [Test]
        public void GetHashCode_ShouldBeDifferent_WhenObjectsDifferent()
        {
            var httpRequest1 = _fixture.Create<RequestRegistration>();
            var httpRequest2 = _fixture.Create<RequestRegistration>();

            httpRequest1.GetHashCode().Should().NotBe(httpRequest2.GetHashCode());

        }
    }
}