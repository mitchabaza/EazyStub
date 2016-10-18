using Latsos.Shared;

namespace Latsos.Core
{
    public interface IRequestMatcher
    {
        RequestRegistration Match(RequestRegistration[] matchingRequests, HttpRequestModel requestMessage);
    }
}