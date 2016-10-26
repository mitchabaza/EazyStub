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
        private HttpResponseModel _responseModel = new HttpResponseModel();

        public ResponseBuilder(StubBuilder stubBuilder)
        {
            Builder = stubBuilder;
        }

        public ResponseBuilderFinisher Body(string content, string mediatype, string charSet )
        {

            _responseModel.Body.Data = content;
            _responseModel.Body.ContentType.MediaType = mediatype;
            _responseModel.Body.ContentType.CharSet= charSet;

            return new ResponseBuilderFinisher(this);
        }
        public ResponseBuilderFinisher Body(string content)
        {

            _responseModel.Body.Data = content;
            _responseModel.Body.ContentType.MediaType = null;
            _responseModel.Body.ContentType.CharSet = null;
            return new ResponseBuilderFinisher(this);
        }

        public ResponseBuilderFinisher StatusCode(HttpStatusCode code)
        {
            _responseModel.StatusCode = code;
            return new ResponseBuilderFinisher(this);
        }

        public ResponseBuilderFinisher StatusCode(int code)
        {
            _responseModel.StatusCode = (HttpStatusCode) code;
            return new ResponseBuilderFinisher(this);
        }

      

        public ResponseBuilderFinisher Header(string key, string value)
        {
            Ensure.That(key).IsNotEmpty();
            Ensure.That(value).IsNotEmpty();

            _responseModel.Headers.Add(key, value);
            return new ResponseBuilderFinisher(this);
        }

        protected internal HttpResponseModel BuildResponse()
        {
            return _responseModel;
        }


        internal void Clear()
        {
            _responseModel = new HttpResponseModel();
        }
    }
}