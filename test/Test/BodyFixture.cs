using EasyStub.Common;
using EasyStub.Test.Util;
using FluentAssertions;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace EasyStub.Test
{
    [TestFixture]
    public class BodyFixture
    {

        [Test]
        public void Equals_ShoulReturnFalse_WhenAnyFieldDifferent()
        {
            var fixture = new Fixture();
            var body1 = fixture.Create<Body>();
            var body2 = fixture.Create<Body>();
            body1.ShouldNotEqual(body2);
        }
        [Test]
        public void Equals_ShoulReturnTrue_WhenAllFieldsSame()
        {
            var fixture = new Fixture();
            var body1 = fixture.Create<Body>();
            var body2 = body1.Copy();
            body1.ShouldEqual(body2);
        }


        [Test]
        public void GetHashCode_ShoulReturnDifferentValue_WhenAnyFieldDifferent()
        {
            var fixture = new Fixture();
            var body1 = fixture.Create<Body>();
            var body2 = fixture.Create<Body>();
            body1.GetHashCode().Should().NotBe(body2.GetHashCode());
        }
        [Test]
        public void GetHashCode_ShoulReturnSameValue_WhenAllFieldsSame()
        {
            var fixture = new Fixture();
            var body1 = fixture.Create<Body>();
            var body2 = body1.Copy();
            body1.GetHashCode().Should().Be(body2.GetHashCode());
        
        
    }
    }
}