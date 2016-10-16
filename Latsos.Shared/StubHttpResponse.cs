using System.Net;
using System.Net.Http.Headers;

namespace Latsos.Shared
{
    /// <summary>
    /// The HTTP response returned when a particular RequestRegistration is matched
    /// </summary>
    public class StubHttpResponse
    {
        public byte[] Contents { get; set; }
        public string ContentType { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public Headers Headers { get; set; }
    }
}