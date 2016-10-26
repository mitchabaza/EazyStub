using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using Latsos.Shared;
using Latsos.Shared.Request;
using Latsos.Shared.Response;
using Microsoft.Owin.Logging;

namespace Latsos.Core
{
    public class RequestEvaluator : IRequestEvaluator
    {
        private readonly IBehaviorRepository _repository;
        private readonly IRequestMatcher _matcher;
        private readonly ILogger _logger;

        public RequestEvaluator(IBehaviorRepository repository,
            IRequestMatcher matcher,ILogger logger)
        {
            _repository = repository;

            _matcher = matcher;
            _logger = logger;
        }

   

        public HttpResponseModel FindRegisteredResponse(HttpRequestModel request)
        {
            _logger.WriteVerbose(request.ToString());
            var matchingRequests = _repository.FindByLocalPath(request.LocalPath);

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
            return _repository.Get(matchingRequest);
        }
    }
}