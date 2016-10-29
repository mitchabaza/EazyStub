using EasyStub.Common.Request;
using EasyStub.Test.Util;
using FluentAssertions;
using NUnit.Framework;

namespace EasyStub.Test
{
    public class MethodFixture
    {
        [Test]
        public void Equals_ShouldReturnTrue_WhenSame()
        {

            var method1 = Method.Delete;
            var method2 = Method.Delete;

            method1.ShouldEqual(method2);
            method1.GetHashCode().Should().Be(method2.GetHashCode());


        }

        [Test]
        public void Equals_ShouldReturnTrue_WhenNotSame()
        {

            var method1 = Method.Delete;
            var method2 = Method.Get;

            method1.ShouldNotEqual(method2);
            method1.GetHashCode().Should().NotBe(method2.GetHashCode());


        }
    }
}