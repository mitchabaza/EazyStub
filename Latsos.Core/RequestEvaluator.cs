using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;

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

        public HttpResponseMessage FindRegisteredResponse(HttpRequestMessage requestMessage)
        {
            var matchingRequests =
                _repository.FindByLocalPath(
                    requestMessage.RequestUri.LocalPath.Replace(_environment.ApplicationVirtualPath, ""));

            //no match found
            if (matchingRequests == null || matchingRequests.Length == 0)
            {
                return null;
            }

            var matchingRequest = _matcher.Match(matchingRequests, TransformRequest(requestMessage));
            if (matchingRequest == null)
            {
                return null;
            }
            //we found a matching request , so return the mocked response 
            return _modelTransformer.Transform(_repository.Remove(matchingRequest));
        }

        private HttpRequest TransformRequest(HttpRequestMessage requestMessage)
        {
            return _modelTransformer.Transform(requestMessage);
        }

        //private HttpRequestRegistration GetMatchingRequest(HttpRequestRegistration[] possibleMatches,
        //    HttpRequest currentRequest)
        //{
        //    return possibleMatches.FirstOrDefault(
        //        m =>
        //            (m.Body.Any || m.Body.Value.Equals(currentRequest.Body)) &&
        //            (m.Method.Any || m.Method.Value.Equals(currentRequest.Method)) &&
        //            (m.Headers.Any || m.Headers.Value.Equals(currentRequest.Headers) &&
        //             (m.Query.Any || m.Query.Value.Equals(currentRequest.Query)) &&
        //             (m.Port.Any || m.Port.Value.Equals(currentRequest.Port))
        //                ));
        //}
    }
}