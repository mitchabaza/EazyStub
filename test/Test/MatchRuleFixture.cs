using System;
using EasyStub.Common;
using EasyStub.Test.Util;
using FluentAssertions;
using NUnit.Framework;

namespace EasyStub.Test
{
    [TestFixture]
    public class MatchRuleFixture
    {
        private class Value : IEquatable<Value>
        {
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;
                return Equals((Value) obj);
            }

            public override int GetHashCode()
            {
                return Property?.GetHashCode() ?? 0;
            }

            public string Property { private get; set; }

            public bool Equals(Value other)
            {
                if (other != null && other.Property.Equals(Property))
                {
                    return true;
                }
                return false;
            }
        }


        [Test]
        public void GetHashCode_ShouldReturnDiffValue_WhenObjectsDifferent()
        {
            var matchRule1 = new MatchRule<Value>(true, new Value() { Property = "A" });
            var matchRule2 = new MatchRule<Value>(true, new Value() { Property = "B" });

            matchRule1.GetHashCode().Should().NotBe(matchRule2.GetHashCode());
        }
        [Test]
        public void GetHashCode_ShouldReturnSameValue_WhenObjectsSame()
        {

            var matchRule1 = new MatchRule<Value>(true, new Value() { Property = "A" });
            var matchRule2 = new MatchRule<Value>(true, new Value() { Property = "A" });

            matchRule1.GetHashCode().Should().Be(matchRule2.GetHashCode());
        }

       
        [Test]
        public void Equals_ShouldReturnTrue_WhenAnyAndValueSame()
        {
            var matchRule1 = new MatchRule<Value>(true, new Value() {Property = "A"});
            var matchRule2 = new MatchRule<Value>(true, new Value() {Property = "A"});
            matchRule2.ShouldEqual(matchRule1);

            matchRule1 = new MatchRule<Value>(false, new Value() {Property = "Z"});
            matchRule2 = new MatchRule<Value>(false, new Value() {Property = "Z"});
            matchRule2.ShouldEqual(matchRule1);
        }

        [Test]
        public void Equals_ShouldReturnTrue_WhenAnySameAndValueNull()
        {
            var matchRule1 = new MatchRule<Value>(true, null);
            var matchRule2 = new MatchRule<Value>(true, null);
            matchRule2.ShouldEqual(matchRule1);

            matchRule1 = new MatchRule<Value>(false, new Value() {Property = "d"});
            matchRule2 = new MatchRule<Value>(true, null);
            matchRule2.ShouldNotEqual(matchRule1);
        }
    }
}