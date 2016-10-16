using System;
using System.Collections.Generic;

namespace Latsos.Shared
{
    public class Headers : IEquatable<Headers>
    {
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Headers) obj);
        }

        public override int GetHashCode()
        {
            return Dictionary?.GetHashCode() ?? 0;
        }

        internal Dictionary<string, string> Dictionary { get; }


        public Headers()
        {
            Dictionary = new Dictionary<string, string>();
        }

        public void Add(string key, string value)
        {
            if (Dictionary.ContainsKey(key))
            {
                Dictionary[key]+=(value);
            }
            else
            {
                Dictionary.Add(key, value);
            }
        }


        public bool Equals(Headers other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return DictionaryComparer.CheckEquality(this.Dictionary, other.Dictionary);
        }
    }
}