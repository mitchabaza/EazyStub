using System.CodeDom;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Routing;
using Latsos.Core;
using Latsos.Web.Controllers;

namespace Latsos.Web
{
    /// <summary>
    /// Intercepts all requests to the server and attemps to match them against requests that have been registered using <see cref="StubController"/>
    /// </summary>
    public class MockRequestHandler : DelegatingHandler
    {
        private readonly IRequestEvaluator _evaluator;

        public MockRequestHandler(IRequestEvaluator evaluator)
        {
            _evaluator = evaluator;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var response = _evaluator.FindRegisteredResponse(request);
            if (response != null)
            {
                var task = new TaskCompletionSource<HttpResponseMessage>();

                task.SetResult(response);

                return task.Task;
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}