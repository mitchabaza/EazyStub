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
        private readonly IHostingEnvironment _environment;
        private readonly IRequestMatcher _matcher;
        private readonly IModelTransformer _modelTransformer;

        public RequestEvaluator(IBehaviorRepository repository,
            IModelTransformer modelTransformer,
            IHostingEnvironment environment,
            IRequestMatcher matcher)
        {
            _repository = repository;
            _environment = environment;
            _matcher = matcher;
            _modelTransformer = modelTransformer;
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
        //    //we found a matching request , so return the mocked ResponseModel 
        //    return _modelTransformer.Transform(_repository.Unregister(matchingRequest));
        //}

        public HttpResponseModel FindRegisteredResponse(HttpRequestModel request)
        {
            var matchingRequests =
                _repository.FindByLocalPath(request.LocalPath.Replace(_environment.ApplicationVirtualPath, ""));

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
            //we found a matching request , so return the mocked ResponseModel 
            return _repository.Unregister(matchingRequest);
        }
    }
}