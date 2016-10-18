using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Latsos.Shared
{
    public class Headers : IEquatable<Headers> , IEnumerable<Headers.Header>
    {
        public class Header
        {
            public Header(string key, string value)
            {
                Key = key;
                Value = value;
            }

            public string Key { get; }
            public string Value { get; }
        }
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
                Dictionary[key]+=","+value;
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


        public IEnumerator<Header> GetEnumerator()
        {
            return Dictionary.Select(s => new Header(s.Key, s.Value)).GetEnumerator();
        }

        public override string ToString()
        {
            if (Dictionary.Count == 0)
            {
                return string.Empty;
            }
            return Dictionary.Select(kvp => $"key: {kvp.Key} value:{kvp.Value}") .Aggregate((curr, next)=> curr + "," + next);
        }
    }
}