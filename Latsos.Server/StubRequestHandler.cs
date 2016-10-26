using System.CodeDom;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using Latsos.Core;
using Latsos.Web.Controllers;
using Microsoft.Owin.Logging;

namespace Latsos.Web
{
    /// <summary>
    /// Intercepts all requests to the server and attemps to match them against requests that have been registered using <see cref="StubController"/>
    /// </summary>
    public class StubRequestHandler : DelegatingHandler
    {
        private readonly IRequestEvaluator _evaluator;
        private readonly IModelTransformer _transformer;
        private readonly ILogger _logger;

        public StubRequestHandler(IRequestEvaluator evaluator, IModelTransformer transformer, ILogger logger)
        {
            _evaluator = evaluator;
            _transformer = transformer;
            _logger = logger;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {

            if (RouteIsReal(request))
            {
                return base.SendAsync(request, cancellationToken);
            }
     
            var response = _evaluator.FindRegisteredResponse(_transformer.Transform(request));
            if (response != null)
            {
                var task = new TaskCompletionSource<HttpResponseMessage>();

                task.SetResult(_transformer.Transform(response));

                return task.Task;
            }
            return base.SendAsync(request, cancellationToken);
        }

        private bool RouteIsReal(HttpRequestMessage request)
        {
            return ((IHttpRouteData[])request.GetConfiguration().Routes.GetRouteData(request)?.Values["MS_SubRoutes"])?.FirstOrDefault()?.Route?.RouteTemplate!=null;
        }
    }
}