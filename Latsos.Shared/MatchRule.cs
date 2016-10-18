using System;
using System.Collections.Generic;
using System.Reflection;

namespace Latsos.Shared
{
    public class MatchRule<T> : IEquatable<MatchRule<T>> where T :  IEquatable<T>
    {
        private bool _any;

        public MatchRule(T value)
        {
            Any = false;
            if (Equals(value, default(T)))
            {
                throw new ArgumentOutOfRangeException("you must specify a non default value when Any is false", null as Exception);
            }
            Value = value;
        }

        public MatchRule(bool any, T value)
        {
            Any = any;
            if (any == false && Equals(value, default(T)))
            {
                throw new ArgumentOutOfRangeException("you must specify a non default value when Any is false",null as Exception);
            }
            Value = value;
        }

       

        public bool Any
        {
            get { return _any; }
            set
            {
                _any = value;
                Value = default(T);
            }
        }

        public T Value { get; set; }

        public static MatchRule<T> Default = new MatchRule<T>(true, default(T));


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MatchRule<T>) obj);
        }

        public virtual bool Equals(MatchRule<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return   (other.Any.Equals(this.Any) && other.Value != null && this.Value != null && other.Value.Equals(Value)) ||
                     other.Any.Equals(this.Any) && Equals(other.Value, default(T)) && Equals(Value, default(T));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_any.GetHashCode()*397) ^ EqualityComparer<T>.Default.GetHashCode(Value);
            }
        }
    }
}