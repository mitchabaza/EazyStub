using System;
using System.Collections.Generic;
using System.Text;

namespace EasyStub.Common.Request
{
    public class QueryString:IEquatable<QueryString>
    {
        

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((QueryString) obj);
        }

        public override int GetHashCode()
        {
            return QueryStrings?.GetHashCode() ?? 0;
        }

        internal Dictionary<string, string> QueryStrings { get; }

        public QueryString()
        {
            QueryStrings = new Dictionary<string, string>();
        }

        public void Add(string key, string value)
        {
            QueryStrings.Add(key, value);
        }

        public override string ToString()
        {
            if (QueryStrings.Count == 0)
            {
                return "";
            }
            var sb = new StringBuilder();
            sb.Append("?");
            foreach (var key in QueryStrings.Keys)
            {
                sb.Append($"{key}={QueryStrings[key]}&");
            }
            
            return sb.ToString().Substring(0,sb.Length-1);
        }

       public  bool Equals(QueryString other)
        {
            if (other == null)
            {
                return false;
            }
            return DictionaryComparer.CheckEquality(QueryStrings, other.QueryStrings);
        }
    }
}