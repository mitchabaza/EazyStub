using System;
using System.Net.Http;

namespace Latsos.Shared
{
    public class HttpRequestRegistration : IEquatable<HttpRequestRegistration>
    {
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((HttpRequestRegistration) obj);
        }

        public HttpRequestRegistration Clone()
        {
            return new HttpRequestRegistration()
            {
                Body = this.Body,
                Headers = this.Headers,
                LocalPath = this.LocalPath,
                Method = this.Method,
                Port = this.Port,
                Query = this.Query
            };
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Port?.GetHashCode() ?? 0;
                hashCode = (hashCode*397) ^ (Body?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (Headers?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (LocalPath?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (Query?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (Method?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        public HttpRequestRegistration()
        {
            Port = new MatchRule<int>(true);
            Body = new MatchRule<Body2>(true);
            Headers = new MatchRule<Headers>(true);
            Query = new MatchRule<string>(true);
            Method = new MatchRule<HttpMethod>(true);
        }

        public MatchRule<int> Port { get; set; }
        public MatchRule<Body2> Body { get; set; }
        public MatchRule<Headers> Headers { get; set; }
        public string LocalPath { get; set; }
        public MatchRule<string> Query { get; set; }
        public MatchRule<HttpMethod> Method { get; set; }

        public bool Equals(HttpRequestRegistration other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Port.Equals(Port) && other.Body.Equals(Body)
                   && other.Headers.Equals(Headers) &&
                   string.Equals(LocalPath, other.LocalPath)
                   && other.Query.Equals(Query) &&
                   other.Method.Equals(Method);
        }
    }
}