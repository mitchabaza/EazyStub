using EasyStub.Common.Request;
using EasyStub.Test.Util;
using FluentAssertions;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace EasyStub.Test
{
    [TestFixture]
    public class RequestRegistrationFixture
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void GetHashCode_ShouldBeSame_WhenObjectsSame()
        {
            var httpRequest3 = _fixture.Create<RequestRegistrationModel>();
            var httpRequest4 = httpRequest3.Copy();


            httpRequest3.GetHashCode().Should().Be(httpRequest4.GetHashCode());

        }

     
        [Test]
        public void GetHashCode_ShouldBeDifferent_WhenObjectsDifferent()
        {
            var httpRequest1 = _fixture.Create<RequestRegistrationModel>();
            var httpRequest2 = _fixture.Create<RequestRegistrationModel>();

            httpRequest1.GetHashCode().Should().NotBe(httpRequest2.GetHashCode());

        }
    }
}