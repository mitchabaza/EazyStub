using System;
using EnsureThat;

namespace EasyStub.Common.Request
{
    public class RequestRegistrationModel : IEquatable<RequestRegistrationModel>
    {
        private string _localPath;

        internal RequestRegistrationModel()
        {
            Port = new MatchRule<int>(true, default(int));
            Body = new MatchRule<Body>(true, default(Body));
            Headers = new MatchRule<Headers>(true, default(Headers));
            Query = new MatchRule<string>(true, default(string));
            Method = new MatchRule<Method>(true, default(Method));
        }

        public RequestRegistrationModel(string localPath, MatchRule<int> port, MatchRule<Body> body, MatchRule<Headers> headers, MatchRule<string> query, MatchRule<Method> method)
        {
            LocalPath = localPath;
            Port = port;
            Body = body;
            Headers = headers;
            Query = query;
            Method = method;
        }

        public MatchRule<int> Port { get; set; }
        public MatchRule<Body> Body { get; set; }
        public MatchRule<Headers> Headers { get; set; }

        public string LocalPath
        {
            get { return _localPath; }
            set
            {
                Ensure.That(value).IsNotEmpty();
                if (value.Trim().Substring(0, 1) != "/")
                {
                    value = "/" + value;
                }
                else
                {
                    value = value.Trim();
                }
                _localPath = value;
            }
        }

        public MatchRule<string> Query { get; set; }

        public MatchRule<Method> Method { get; set; }

        public bool Equals(RequestRegistrationModel other)
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
            if (obj.GetType() != GetType()) return false;
            return Equals((RequestRegistrationModel) obj);
        }

        public RequestRegistrationModel Clone()
        {
            return new RequestRegistrationModel(LocalPath, Port, Body, Headers, Query, Method);
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