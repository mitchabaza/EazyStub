using System.Net.Http;
using Latsos.Shared;

namespace Latsos.Core
{
    public class ModelTransformer : IModelTransformer
    {
        public HttpResponseMessage Transform(StubHttpResponse response)
        {
             return new HttpResponseMessage(response.StatusCode) {};
        }

        public HttpRequest Transform(HttpRequestMessage request)
        {
            throw new System.NotImplementedException();
        }
    }
}