using System.Diagnostics.Contracts;
using Latsos.Shared;

namespace Latsos.Client
{
    public class ResponseBuilderFinisher  
    {
        private readonly ResponseBuilder _inner;

        public ResponseBuilderFinisher(ResponseBuilder inner)
        {
            _inner = inner;
        }

        public BehaviorRegistrationRequest Build()
        {
            return new BehaviorRegistrationRequest()
            {
                RequestRegistration = _inner.Builder.RequestBuilder.Build(),
                Response = _inner.BuildResponse()
            };
        }
    }
}