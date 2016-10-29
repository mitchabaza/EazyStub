using System;
using EasyStub.Common;
using EasyStub.Common.Request;
using EnsureThat;

namespace EasyStub.Client
{
    public class RequestBuilderFinisher : RequestBuilder
    {
        private RequestRegistrationModel _registrationModel = new RequestRegistrationModel();

        public RequestBuilderFinisher WithQueryString(string key, string value)
        {
            Ensure.That(key).IsNotEmpty();
            Ensure.That(value).IsNotEmpty();
            _registrationModel.Query.Any = false;

            if (_registrationModel.Query.Value != null)
            {
                _registrationModel.Query.Value += "&";
            }
            else
            {
                _registrationModel.Query.Value = "?";
            }
            _registrationModel.Query.Value += $"{key}={value}";
            return this;
        }

        public RequestBuilderFinisher WithPort(int port)
        {
            Ensure.That(port).IsInRange(0, short.MaxValue);
            _registrationModel.Port.Any = false;
            _registrationModel.Port.Value = port;

            return this;
        }

        public RequestBuilderFinisher WithMethod(Method method)
        {
            Ensure.That(method).IsNotNull();
            _registrationModel.Method.Any = false;
            _registrationModel.Method.Value = method;
            return this;
        }

        public RequestBuilderFinisher WithHeader(string key, string value)
        {
            Ensure.That(key).IsNotNull();
            Ensure.That(value).IsNotNull();
            _registrationModel.Headers.Any = false;
            if (_registrationModel.Headers.Value == null)
            {
                _registrationModel.Headers.Value = new Headers();
            }
            _registrationModel.Headers.Value.Add(key, value);
            return this;
        }

        public RequestBuilderFinisher WithBody(string data, string mediaType, string charset)
        {
            _registrationModel.Body.Any = false;
            _registrationModel.Body.Value = new Body()
            {
                ContentType = new ContentType() {CharSet = charset, MediaType = mediaType}
            };
            return this;
        }

        internal RequestRegistrationModel Build()
        {
            if (string.IsNullOrEmpty(_registrationModel.LocalPath))
            {
                throw new InvalidOperationException("WithPath property value must be supplied");
            }
            var reg = _registrationModel;
            Clear();
            return reg;
        }

        internal void Clear()
        {
            _registrationModel = new RequestRegistrationModel();
        }

        public RequestBuilderFinisher WithBody(string data, string mediaType)
        {
            _registrationModel.Body.Any = false;
            _registrationModel.Body.Value = new Body() {Data = data, ContentType = new ContentType() {MediaType = mediaType}};
            return this;
        }

        public ResponseBuilder WillReturnResponse() => StubBuilder.ResponseBuilder;

        public RequestBuilderFinisher(StubBuilder stubBuilder) : base(stubBuilder)
        {
            StubBuilder.RequestBuilderFinisher = this;
        }

        public RequestBuilderFinisher(StubBuilder stubBuilder, string path) : this(stubBuilder)
        {
            _registrationModel.LocalPath = path;
        }
    }
}