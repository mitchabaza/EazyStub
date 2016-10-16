using System.Net.Http;
using Latsos.Shared;

namespace Latsos.Core
{
    public class HttpRequest
    {
        public Body Body { get; set; }
        public HttpMethod Method { get; set; }
        public Headers Headers { get; set; }
        public string Query { get; set; }
        public int Port { get; set; }
    }
    public interface IModelTransformer
    {
        HttpResponseMessage Transform(StubHttpResponse response);
        HttpRequest Transform(HttpRequestMessage request);
    }
}