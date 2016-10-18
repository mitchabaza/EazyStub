using System.Net;
using System.Net.Http.Headers;

namespace Latsos.Shared
{
    /// <summary>
    /// The HTTP Response returned when a particular <see cref="RequestRegistration"></see> is matched
    /// </summary>
    public class HttpResponseModel
    {
        public string Contents { get; set; }
        public string ContentType { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public Headers Headers { get; set; }
    }
}