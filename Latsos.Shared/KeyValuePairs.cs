using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Latsos.Shared
{

    public abstract class KeyValuePairs : IEquatable<KeyValuePairs>
    {
        public void Clear()
        {
            Dictionary.Clear();
        }

       

        public Dictionary<string, string> Dictionary { get; }

        public KeyValuePairs(Dictionary<string, string> dictionary)
        {
            Dictionary = dictionary;
        }

        public KeyValuePairs()
        {
            Dictionary = new Dictionary<string, string>();
        }

        public abstract void Add(string key, string value);

        public bool Equals(KeyValuePairs other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return DictionaryComparer.CheckEquality(this.Dictionary, other.Dictionary);
        }
    }
}