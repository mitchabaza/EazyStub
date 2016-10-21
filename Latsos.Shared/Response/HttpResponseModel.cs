using System.Net;
using System.Net.Http.Headers;

namespace Latsos.Shared
{
    /// <summary>
    /// The HTTP Response returned when a particular <see cref="RequestRegistration"></see> is matched
    /// </summary>
    public class HttpResponseModel
    {
 
        public Body Body { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public Headers Headers { get; set; }

        public HttpResponseModel()
        {
            Headers = new Headers();
            Body=new Body();
        }

        
    }
}