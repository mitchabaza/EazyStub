using System;

namespace EasyStub.Common
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
                if (_any)
                {
                    Value = default(T);

                }
            }
        }

        public T Value { get; set; }

        public static MatchRule<T> Default = new MatchRule<T>(true, default(T));


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((MatchRule<T>) obj);
        }

        public virtual bool Equals(MatchRule<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return   (other.Any.Equals(Any) && other.Value != null && Value != null && other.Value.Equals(Value)) ||
                     other.Any.Equals(Any) && Equals(other.Value, default(T)) && Equals(Value, default(T));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_any.GetHashCode()*397) ^ (Equals(Value,default(T))?0:Value.GetHashCode());
            }
        }

        public override string ToString()
        {
            return $"Any: {Any}, Value: {Value}";
        }
    }
}