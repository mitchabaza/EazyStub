using System;
using System.Net.Http;
using System.Text;
using EnsureThat;
using Latsos.Shared;
using Latsos.Shared.Request;

namespace Latsos.Client
{
    public class RequestBuilder
    {
        private readonly StubBuilder _stubBuilder;

        internal StubBuilder StubBuilder => _stubBuilder;
       
        public RequestBuilder(StubBuilder stubBuilder)
        {
            _stubBuilder = stubBuilder;
        }


        public RequestBuilderFinisher Path(string path)
        {
            return new RequestBuilderFinisher(this.StubBuilder, path);
        }

      

    }
}