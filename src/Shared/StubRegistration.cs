using EasyStub.Common.Request;
using EasyStub.Common.Response;

namespace EasyStub.Common
{
   
    public class StubRegistration
    {
        public HttpResponseModel Response { get; set; }
        public RequestRegistrationModel Request { get; set; }

        public override string ToString()
        {
            return $"Response: {Response}, Request: {Request}";
        }
    }

    
}