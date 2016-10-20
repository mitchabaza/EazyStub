using System.Net.Http;
using EnsureThat;
using Latsos.Shared;

namespace Latsos.Client
{
    public class RequestBuilder
    {
        private readonly StubBuilder _stubBuilder;
        private MatchRule<Method> _method = new MatchRule<Method>(true, null);
        private MatchRule<int> _port = new MatchRule<int>(true, 0);
        private MatchRule<string> _queryStringMatch = new MatchRule<string>(true, null);
        private MatchRule<Body> _content = new MatchRule<Body>(true, null);
        private QueryString _queryString = new QueryString();
        private string _path;
        private MatchRule<Headers> _headers = new MatchRule<Headers>(true, null);

        public RequestBuilder(StubBuilder stubBuilder)
        {
            _stubBuilder = stubBuilder;
        }


        public RequestBuilder QueryString(string key, string value)
        {
            Ensure.That(key).IsNotEmpty();
            Ensure.That(value).IsNotEmpty();
            _queryStringMatch.Any = false;
            _queryString.Add(key, value);
            _queryStringMatch.Value = _queryString.ToString();
            return this;
        }

        public RequestBuilder Path(string path)
        {
            Ensure.That(path).IsNotEmpty();
            if (path.Trim().Substring(0, 1) != "/")
            {
                _path = "/" + path;
            }
            else
            {
                _path = path.Trim();
            }

            return this;
        }


        public RequestBuilder Port(int port)
        {
            Ensure.That(port).IsInRange(0, 65535);
            _port.Any = false;
            _port.Value = port;

            return this;
        }

        public RequestBuilder Method(Method method)
        {
            Ensure.That(method).IsNotNull();
            _method.Any = false;
            _method.Value = method;
            return this;
        }

        public RequestBuilder Header(string key, string value)
        {
            Ensure.That(key).IsNotNull();
            Ensure.That(value).IsNotNull();
            if (_headers.Value == null)
            {
                _headers.Value = new Headers();
            }
            _headers.Value.Add(key, value);
            return this;
        }


        public ResponseBuilder Returns()
        {
            return this._stubBuilder.ResponseBuilder;
        }

        public RequestRegistration Build()
        {
            Ensure.That(_path, "LocalPath").IsNotNullOrWhiteSpace();
            return new RequestRegistration()
            {
                LocalPath = _path,
                Query = _queryStringMatch,
                Headers = _headers,
                Port = _port,
                Method = _method,
                Body = _content
            };
        }

        public void Clear()
        {
            _method = new MatchRule<Method>(true, null);
            _port = new MatchRule<int>(true, 0);
            _queryStringMatch = new MatchRule<string>(true, null);
            _content = new MatchRule<Body>(true, null);
            _queryString = new QueryString();
            _path = "";
            _headers = new MatchRule<Headers>(true, null);
        }
    }
}