using System;
using System.Collections.Generic;
using System.Linq;
using EnsureThat;

namespace Latsos.Shared
{
    public class Headers : KeyValuePairs,IEquatable<Headers>
    {

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Headers)obj);
        }

        public override int GetHashCode()
        {
            return Dictionary?.Select(k => k.Key.GetHashCode() ^ k.Value.GetHashCode()).Aggregate(1, (c, n) => c * n) ?? 0;
        }

        public override void Add(string key, string value)
        {
            Ensure.That(key).IsNotNullOrEmpty();
            Ensure.That(value).IsNotNullOrEmpty();

            if (Dictionary.ContainsKey(key))
            {
                Dictionary[key] += "," + value;
            }
            else
            {
                Dictionary.Add(key, value);
            }
        }

        public Headers(Dictionary<string, string> dictionary) : base(dictionary)
        {
        }

        public Headers()
        {
        }


        public bool Equals(Headers other)
        {
            return DictionaryComparer.CheckEquality(this.Dictionary, other?.Dictionary);
        }

        public override string ToString()
        {
            if (Dictionary.Count == 0)
            {
                return string.Empty;
            }
            return
                Dictionary.Select(kvp => $"key: {kvp.Key} value:{kvp.Value}")
                    .Aggregate((curr, next) => curr + "," + next);
        }
    }
}