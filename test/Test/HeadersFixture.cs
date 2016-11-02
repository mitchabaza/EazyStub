using System;
using EasyStub.Common;
using EasyStub.Test.Util;
using FluentAssertions;
using NUnit.Framework;

namespace EasyStub.Test
{
    [TestFixture]
    public class HeadersFixture
    {
        [Test]
        public void Equals_ShouldReturnTrue_WhenKeyAndValueSame()
        {
            var headers1 = new Headers();
            headers1.Add("id", "b");
            var headers2 = new Headers();
            headers2.Add("id", "b");
            headers2.ShouldEqual(headers1);
            headers2.GetHashCode().Should().Be(headers1.GetHashCode());
        }

        [Test]
        public void Equals_ShouldReturnFalse_WhenKeySameButValueDifferent()
        {
            var headers1 = new Headers();
            headers1.Add("id", "b");
            var headers2 = new Headers();
            headers1 = new Headers();
            headers1.Add("id", "x");

            headers2.ShouldNotEqual(headers1);
            headers2.GetHashCode().Should().NotBe(headers1.GetHashCode());
        }
        [Test]
        public void Equals_ShouldReturnTrue_WhenKeyAndValueExistsInSecond()
        {
            var headers1 = new Headers();
            headers1.Add("a", "1");
            headers1.Add("b", "2");
            var headers2 = new Headers();
            headers1 = new Headers();
            headers1.Add("a", "1");

            headers2.ShouldEqual(headers1);
        }
        [Test]
        public void Add_ShouldThrow_WhenValueIsNull()
        {
            var headers1 = new Headers();
            Action method = () => headers1.Add("id", null);
            method.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Add_ShouldThrow_WhenKeyIsNull()
        {
            var headers1 = new Headers();
            Action method = () => headers1.Add(null, "");
            method.ShouldThrow<ArgumentNullException>();
        }
    }
}