using EasyStub.Common.Request;

namespace EasyStub.Core
{
    public interface IRequestMatcher
    {
        RequestRegistrationModel Match(RequestRegistrationModel[] matchingRequests, HttpRequestModel requestMessage);
    }
}