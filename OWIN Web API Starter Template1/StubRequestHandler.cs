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
    public class StubRequestHandler : DelegatingHandler
    {
        private readonly IRequestEvaluator _evaluator;
        private readonly IModelTransformer _transformer;

        public StubRequestHandler(IRequestEvaluator evaluator, IModelTransformer transformer)
        {
            _evaluator = evaluator;
            _transformer = transformer;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {

            var response = _evaluator.FindRegisteredResponse(_transformer.Transform(request));
            if (response != null)
            {
                var task = new TaskCompletionSource<HttpResponseMessage>();

                task.SetResult(_transformer.Transform(response));

                return task.Task;
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}