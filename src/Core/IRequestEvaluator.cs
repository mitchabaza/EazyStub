using EasyStub.Common.Request;
using EasyStub.Common.Response;

namespace EasyStub.Core
{
    public interface IRequestEvaluator
    {
      
        HttpResponseModel FindRegisteredResponse(HttpRequestModel request);
    }
}