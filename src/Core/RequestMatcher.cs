using System;
using System.Linq;
using EasyStub.Common.Request;

namespace EasyStub.Core
{
    public class RequestMatcher : IRequestMatcher
    {
        public RequestRegistrationModel Match(RequestRegistrationModel[] matchingRequests, HttpRequestModel requestMessage)
        {
            if (matchingRequests == null || matchingRequests.Length == 0)
            {
                throw new ArgumentException("matchingRequests");
            }
            
           
            return matchingRequests.FirstOrDefault(m => (m.Method.Any || m.Method.Value!=null && m.Method.Value.Equals(requestMessage.Method))
                                                        &&
                                                        (m.Body.Any || m.Body.Value != null && m.Body.Value.Equals(requestMessage.Body))
                                                        &&
                                                        (m.Headers.Any || m.Headers.Value != null && m.Headers.Value.Equals(requestMessage.Headers))
                                                        &&
                                                        (m.Port.Any || m.Port.Value.Equals(requestMessage.Port))
                                                        &&
                                                        (m.Query.Any || m.Query.Value!=null &&  m.Query.Value.Equals(requestMessage.Query))
                                                        &&
                                                        m.LocalPath.Equals(requestMessage.LocalPath)
                );
        }
    }
}