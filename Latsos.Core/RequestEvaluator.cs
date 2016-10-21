using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using Latsos.Shared;

namespace Latsos.Core
{
    public class RequestEvaluator : IRequestEvaluator
    {
        private readonly IBehaviorRepository _repository;
         private readonly IRequestMatcher _matcher;

        public RequestEvaluator(IBehaviorRepository repository,
           
            IHostingEnvironment environment,
            IRequestMatcher matcher)
        {
            _repository = repository;
            
            _matcher = matcher;
        }

        //public HttpResponseMessage FindRegisteredResponse(HttpRequestMessage requestMessage)
        //{
        //    var matchingRequests =
        //        _repository.FindByLocalPath(
        //            requestMessage.RequestUri.LocalPath.Replace(_environment.ApplicationVirtualPath, ""));

        //    //no match found
        //    if (matchingRequests == null || matchingRequests.Length == 0)
        //    {
        //        return null;
        //    }

        //    var matchingRequest = _matcher.Match(matchingRequests, _modelTransformer.Transform(requestMessage));
        //    if (matchingRequest == null)
        //    {
        //        return null;
        //    }
        //    //we found a matching request , so return the mocked Response 
        //    return _modelTransformer.Transform(_repository.Unregister(matchingRequest));
        //}

        public HttpResponseModel FindRegisteredResponse(HttpRequestModel request)
        {
            var matchingRequests =_repository.FindByLocalPath(request.LocalPath);

            //no match found
            if (matchingRequests == null || matchingRequests.Length == 0)
            {
                return null;
            }

            var matchingRequest = _matcher.Match(matchingRequests, request);
            if (matchingRequest == null)
            {
                return null;
            }
            //we found a matching request , so return the mocked Response 
            return _repository.Unregister(matchingRequest);
        }
    }
}