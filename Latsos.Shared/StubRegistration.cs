using System.Threading;
using Latsos.Shared.Request;
using Latsos.Shared.Response;

namespace Latsos.Shared
{
   
    public class StubRegistration
    {
        public HttpResponseModel Response { get; set; }
        public RequestRegistration Request { get; set; }

        public override string ToString()
        {
            return $"Response: {Response}, Request: {Request}";
        }
    }

    
}