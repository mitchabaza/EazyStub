using System.Net.Http;
using System.Text;
using EnsureThat;
using Latsos.Shared;

namespace Latsos.Client
{
    public class RequestBuilder
    {
        private readonly StubBuilder _stubBuilder;

        private RequestRegistration _registration = new RequestRegistration();

        public RequestBuilder(StubBuilder stubBuilder)
        {
            _stubBuilder = stubBuilder;
        }


        public RequestBuilder QueryString(string key, string value)
        {
            Ensure.That(key).IsNotEmpty();
            Ensure.That(value).IsNotEmpty();
            _registration.Query.Any = false;

            if (_registration.Query.Value != null)
            {
                _registration.Query.Value += "&";
            }
            else
            {
                _registration.Query.Value = "?";

            }
                _registration.Query.Value += $"{key}={value}";
            return this;
        }

        public RequestBuilder Path(string path)
        {
            Ensure.That(path).IsNotEmpty();
            if (path.Trim().Substring(0, 1) != "/")
            {
                path = "/" + path;
            }
            else
            {
                path = path.Trim();
            }
            _registration.LocalPath = path;
            return this;
        }


        public RequestBuilder Port(int port)
        {
            Ensure.That(port).IsInRange(0, 65535);
            _registration.Port.Any = false;
            _registration.Port.Value = port;

            return this;
        }

        public RequestBuilder Method(Method method)
        {
            Ensure.That(method).IsNotNull();
            _registration.Method.Any = false;
            _registration.Method.Value = method;
            return this;
        }

        public RequestBuilder Header(string key, string value)
        {
            Ensure.That(key).IsNotNull();
            Ensure.That(value).IsNotNull();
            _registration.Headers.Any = false;
            if (_registration.Headers.Value == null)
            {
                _registration.Headers.Value = new Headers();
            }
            _registration.Headers.Value.Add(key, value);
            return this;
        }


        public ResponseBuilder Returns()
        {
            return this._stubBuilder.ResponseBuilder;
        }

        public RequestRegistration Build()
        {
           
            var reg = _registration;
            Clear();
            return reg;
        }

        internal void Clear()
        {
            //_method = new MatchRule<Method>(true, null);
            //_port = new MatchRule<int>(true, 0);
            //_queryStringMatch = new MatchRule<string>(true, null);
            //_body = new MatchRule<Body>(true, null);
            //_queryString = new QueryString();
            //_path = "";
            //_headers = new MatchRule<Headers>(true, null);
            _registration = new RequestRegistration();
        }

        public RequestBuilder Body(string data, string mediaType, string charset)
        {
            _registration.Body.Any = false;
            _registration.Body.Value = new Body()
            {
                ContentType = new ContentType() {CharSet = charset, MediaType = mediaType}
            };
            return this;
        }

        public RequestBuilder Body(string data, string mediaType)
        {
            _registration.Body.Any = false;
            _registration.Body.Value = new Body() {Data = data, ContentType = new ContentType() {MediaType = mediaType}};
            return this;
        }
    }
}