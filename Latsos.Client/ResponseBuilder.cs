using System;
using System.Collections.Generic;
using System.Net;
using EnsureThat;
using Latsos.Shared;

namespace Latsos.Client
{
    public class ResponseBuilder
    {
        public StubBuilder Builder { get; }
        private string _contentType;
        private string _data;
        private  Headers _headers = new Headers();
        private HttpStatusCode _statusCode = HttpStatusCode.OK;

        public ResponseBuilder(StubBuilder stubBuilder)
        {
            Builder = stubBuilder;
        }

        public ResponseBuilderFinisher Body(string content, string type)
        {
            // Ensure.That(content).IsNotEmpty();
            _data = content;
            _contentType = type;

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

        //public ResponseBuilderFinisher Contents(string contents)
        //{
        //    Ensure.That(contents).IsNotNull();
        //    _data = contents;
        //    return new ResponseBuilderFinisher(this);
        //}

        public ResponseBuilderFinisher Header(string key, string value)
        {
            Ensure.That(key).IsNotEmpty();
            Ensure.That(value).IsNotEmpty();

            _headers.Add(key, value);
            return new ResponseBuilderFinisher(this);
        }

        protected internal HttpResponseModel BuildResponse()
        {
            return new HttpResponseModel()
            {
                Body = new Body() {Data = _data, ContentType = _contentType},
                StatusCode = _statusCode,
                Headers = _headers
            };
        }


        public void Clear()
        {
            _contentType = "";
            _data = "";
            _headers = new Headers();
            _statusCode = HttpStatusCode.OK;
        }
    }
}