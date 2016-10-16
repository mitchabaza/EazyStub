using System;
using System.Collections.Generic;
using System.Net;
using EnsureThat;
using Latsos.Shared;

namespace Latsos.Client
{
    public class ResponseBuilder 
    {
        public MockBuilder Builder { get; }
        private string _contentType;
        private byte[] _contents;
        private readonly Headers _headers = new Headers();
        private HttpStatusCode _statusCode;

        public ResponseBuilder(MockBuilder mockBuilder)
        {
            Builder = mockBuilder;
        }

        public ResponseBuilderFinisher ContentType(string contentType)
        {
            Ensure.That(contentType).IsNotEmpty();
            _contentType = contentType;
            return new ResponseBuilderFinisher(this);
        }

        public ResponseBuilderFinisher StatusCode(HttpStatusCode code)
        {
             _statusCode = code;
            return new ResponseBuilderFinisher(this);
        }
        public ResponseBuilderFinisher StatusCode(int code)
        {
            _statusCode = (HttpStatusCode) code;
            return new ResponseBuilderFinisher(this);
        }
        public ResponseBuilderFinisher Contents(byte[] contents)
        {
            Ensure.That(contents).IsNotNull();
            _contents = contents;
            return new ResponseBuilderFinisher(this);
        }

        public ResponseBuilderFinisher Header(string key, string value)
        {
            Ensure.That(key).IsNotEmpty();
            Ensure.That(value).IsNotEmpty();

            _headers.Add(key, value);
            return new ResponseBuilderFinisher(this);
        }

        protected internal StubHttpResponse BuildResponse()
        {
            return new StubHttpResponse()
            {
                ContentType = _contentType,
                StatusCode = _statusCode,
                Contents = _contents,
                Headers = _headers
            };
        }

       
    }
}