using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using EnsureThat;
using Latsos.Shared;
using Latsos.Shared.Response;


namespace Latsos.Client
{
    public class ResponseBuilder
    {
        protected StubBuilder StubBuilder { get; }
        protected HttpResponseModel ResponseModel;

        public ResponseBuilder(StubBuilder stubStubBuilder, HttpResponseModel model)
        {
            ResponseModel = model;
            StubBuilder = stubStubBuilder;
        }
        public ResponseBuilder(StubBuilder stubStubBuilder ):this(stubStubBuilder, new HttpResponseModel())
        {
            StubBuilder = stubStubBuilder;
        }

        public ResponseBuilderFinisher WithBody(string content, string mediatype, string charSet)
        {
            ResponseModel.Body.Data = content;
            ResponseModel.Body.ContentType.MediaType = mediatype;
            ResponseModel.Body.ContentType.CharSet = charSet;

            return new ResponseBuilderFinisher(StubBuilder, ResponseModel);
        }

        public ResponseBuilderFinisher WithBody(string content)
        {
            ResponseModel.Body.Data = content;
            ResponseModel.Body.ContentType.MediaType = null;
            ResponseModel.Body.ContentType.CharSet = null;

            return new ResponseBuilderFinisher(StubBuilder, ResponseModel);
        }

        public ResponseBuilderFinisher WithStatusCode(HttpStatusCode code)
        {
            ResponseModel.StatusCode = code;

            return new ResponseBuilderFinisher(StubBuilder, ResponseModel);
        }


        public ResponseBuilderFinisher WithHeader(string key, string value)
        {
            Ensure.That(key).IsNotEmpty();
            Ensure.That(value).IsNotEmpty();

            ResponseModel.Headers.Add(key, value);

            return new ResponseBuilderFinisher(StubBuilder, ResponseModel);
        }

        public HttpResponseModel Build()
        {
            return ResponseModel;
        }


        internal void Clear()
        {
            ResponseModel = new HttpResponseModel();
        }
    }
}