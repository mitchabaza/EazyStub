using System.Net.Http;

namespace Latsos.Core
{
    public interface IRequestEvaluator
    {
        HttpResponseMessage FindRegisteredResponse(HttpRequestMessage request);
    }
}