using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Latsos.Web
{
    public class ProxyDelegatingHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            IEnumerable<DelegatingHandler> innerHandlers =
                request.GetDependencyScope()
                    .GetServices(typeof(DelegatingHandler))
                    .OfType<DelegatingHandler>();

            HttpMessageHandler handler = this.InnerHandler;
            foreach (DelegatingHandler innerHandler in innerHandlers)
            {
                innerHandler.InnerHandler = handler;
                handler = innerHandler;
            }

            HttpMessageInvoker invoker = new HttpMessageInvoker(handler, false);
            return invoker.SendAsync(request, cancellationToken);
        }
    }
}