using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using EnsureThat;
using Latsos.Shared;
using Latsos.Shared.Response;


namespace Latsos.Client
{
    public class ResponseBuilder
    {
        internal StubBuilder Builder { get; }
        private string _mediaType;
        private string _data;
        private  Headers _headers = new Headers();
        private HttpStatusCode _statusCode = HttpStatusCode.OK;
        private string _charset;

        public ResponseBuilder(StubBuilder stubBuilder)
        {
            Builder = stubBuilder;
        }

        public ResponseBuilderFinisher Body(string content, string mediatype, string charSet )
        {
            
            _data = content;
            _mediaType = mediatype;
            _charset = charSet;

            return new ResponseBuilderFinisher(this);
        }
        public ResponseBuilderFinisher Body(string content)
        {

            _data = content;
            _mediaType = null;
            _charset = null;
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
                Body = new Body() {Data = _data, ContentType = new ContentType() {MediaType = _mediaType, CharSet =  _charset} },
                StatusCode = _statusCode,
                Headers = _headers
            };
        }


        public void Clear()
        {
            _mediaType = "";
            _data = "";
            _headers = new Headers();
            _statusCode = HttpStatusCode.OK;
        }
    }
}