using Latsos.Shared;
using Latsos.Shared.Request;

namespace Latsos.Core
{
    public interface IRequestMatcher
    {
        RequestRegistration Match(RequestRegistration[] matchingRequests, HttpRequestModel requestMessage);
    }
}