using System.Net.Http;
using Latsos.Shared;

namespace Latsos.Core
{
    public interface IRequestEvaluator
    {
      
        HttpResponseModel FindRegisteredResponse(HttpRequestModel request);
    }
}