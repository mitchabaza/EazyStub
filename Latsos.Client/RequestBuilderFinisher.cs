using System;
using EnsureThat;
using Latsos.Shared;
using Latsos.Shared.Request;

namespace Latsos.Client
{
    public class RequestBuilderFinisher:RequestBuilder
    {
        
        private RequestRegistration _registration = new RequestRegistration();
        
      

        public RequestBuilderFinisher QueryString(string key, string value)
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

        public RequestBuilderFinisher Port(int port)
        {
            Ensure.That(port).IsInRange(0, short.MaxValue);
            _registration.Port.Any = false;
            _registration.Port.Value = port;

            return this;
        }

        public RequestBuilderFinisher Method(Method method)
        {
            Ensure.That(method).IsNotNull();
            _registration.Method.Any = false;
            _registration.Method.Value = method;
            return this;
        }

        public RequestBuilderFinisher Header(string key, string value)
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

        public RequestBuilderFinisher Body(string data, string mediaType, string charset)
        {
            _registration.Body.Any = false;
            _registration.Body.Value = new Body()
            {
                ContentType = new ContentType() { CharSet = charset, MediaType = mediaType }
            };
            return this;
        }

        internal RequestRegistration Build()
        {
            if (string.IsNullOrEmpty(_registration.LocalPath))
            {
                throw new InvalidOperationException("Path property value must be supplied");
            }
            var reg = _registration;
            Clear();
            return reg;
        }

        internal void Clear()
        {

            _registration = new RequestRegistration();
        }
        public RequestBuilderFinisher Body(string data, string mediaType)
        {
            _registration.Body.Any = false;
            _registration.Body.Value = new Body() { Data = data, ContentType = new ContentType() { MediaType = mediaType } };
            return this;
        }

        public ResponseBuilder Response
        {
            get { return this.StubBuilder.ResponseBuilder; }
        }

        public RequestBuilderFinisher(StubBuilder stubBuilder) : base(stubBuilder)
        {
            this.StubBuilder.RequestBuilderFinisher = this;
        }

        public RequestBuilderFinisher(StubBuilder stubBuilder, string path) : this(stubBuilder)
        {
            _registration.LocalPath = path;
        }
    }
}