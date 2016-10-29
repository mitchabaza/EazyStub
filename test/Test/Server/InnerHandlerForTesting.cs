using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace EasyStub.Test.Server
{
    public class InnerHandlerForTesting : DelegatingHandler
    {
        private Func<HttpRequestMessage,
            CancellationToken, Task<HttpResponseMessage>> _handlerFunc;


        public void SetHandler(Func<HttpRequestMessage,
            CancellationToken, Task<HttpResponseMessage>> handlerFunc)
        {
            _handlerFunc = handlerFunc;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return _handlerFunc(request, cancellationToken);
        }

        
    }
}