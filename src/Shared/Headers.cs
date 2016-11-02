using System;
using System.Collections.Generic;
using System.Linq;
using EnsureThat;

namespace EasyStub.Common
{
    public class Headers : KeyValuePairs,IEquatable<Headers>
    {

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
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
            //IIS converts all request headers to lowercase, so let's do the same to ours 
            if (Dictionary.ContainsKey(key.ToLower()))
            {
                Dictionary[key.ToLower()] += "," + value;
            }
            else
            {
                Dictionary.Add(key.ToLower(), value);
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
            return DictionaryComparer.CheckEquality(Dictionary, other?.Dictionary);
        }

        public bool Contains(Headers other)
        {
            if (other == null)
            {
                return false;
            }
            foreach (var key in Dictionary.Keys)
            {
                if (other.Dictionary.ContainsKey(key))
                {
                    if (other.Dictionary[key].Equals(this.Dictionary[key]))
                    {

                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            return true;
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