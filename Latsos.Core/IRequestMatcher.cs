using Latsos.Shared;

namespace Latsos.Core
{
    public interface IRequestMatcher
    {
        HttpRequestRegistration Match(HttpRequestRegistration[] matchingRequests, HttpRequest requestMessage);
    }
}