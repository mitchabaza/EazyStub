using System;

namespace Latsos.Shared.Request
{
    public class RequestRegistration : IEquatable<RequestRegistration>
    {
        private MatchRule<string> _query;

        public RequestRegistration()
        {
            Port = new MatchRule<int>(true,default(int));
            Body = new MatchRule<Body>(true, default(Body));
            Headers = new MatchRule<Headers>(true, default(Headers));
            Query = new MatchRule<string>(true, default(string));
            Method = new MatchRule<Method>(true, default(Method));
        }

        
        public MatchRule<int> Port { get; set; }
        public MatchRule<Body> Body { get; set; }
        public MatchRule<Headers> Headers { get; set; }
        public string LocalPath { get; set; }

        public MatchRule<string> Query
        {
            get { return _query; }
            set
            {
                _query = value;
            }
        }

        public MatchRule<Method> Method { get; set; }

        public bool Equals(RequestRegistration other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Port.Equals(Port) && other.Body.Equals(Body)
                   && other.Headers.Equals(Headers) &&
                   string.Equals(LocalPath, other.LocalPath)
                   && other.Query.Equals(Query) &&
                   other.Method.Equals(Method);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RequestRegistration) obj);
        }

        public RequestRegistration Clone()
        {
            return new RequestRegistration()
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

        public override string ToString()
        {
            return $"Port: {Port}, Body: {Body}, Headers: {Headers}, LocalPath: {LocalPath}, Query: {Query}, Method: {Method}";
        }
    }
}