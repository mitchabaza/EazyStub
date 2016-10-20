using System.Diagnostics.Contracts;
using Latsos.Shared;

namespace Latsos.Client
{
    public class ResponseBuilderFinisher  : ResponseBuilder
    {
        private readonly ResponseBuilder _inner;

        public ResponseBuilderFinisher(ResponseBuilder inner) : base(inner.Builder)
        {
            _inner = inner;
        }

        public StubRegistration Build()
        {
            
            var stubRegistration = new StubRegistration()
            {
                Request = _inner.Builder.RequestBuilder.Build(),
                Response = _inner.BuildResponse()
            };
            _inner.Builder.RequestBuilder.Clear();
            _inner.Builder.ResponseBuilder.Clear();
            return stubRegistration ;
        }
    }
}