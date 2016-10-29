using System;
using System.Collections.Generic;

namespace EasyStub.Common
{

    public abstract class KeyValuePairs : IEquatable<KeyValuePairs>
    {
        public void Clear()
        {
            Dictionary.Clear();
        }

       

        public Dictionary<string, string> Dictionary { get; }

        protected KeyValuePairs(Dictionary<string, string> dictionary)
        {
            Dictionary = dictionary;
        }

        protected KeyValuePairs()
        {
            Dictionary = new Dictionary<string, string>();
        }

        public abstract void Add(string key, string value);

        public bool Equals(KeyValuePairs other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return DictionaryComparer.CheckEquality(Dictionary, other.Dictionary);
        }
    }
}