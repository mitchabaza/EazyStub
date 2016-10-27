using System.Diagnostics.Contracts;
using Latsos.Shared;
using Latsos.Shared.Response;

namespace Latsos.Client
{
    public class ResponseBuilderFinisher  : ResponseBuilder
    {
       

        public StubRegistration Build()
        {
            
            var stubRegistration = new StubRegistration()
            {
                Request = StubBuilder.RequestBuilderFinisher.Build(),
                Response = ResponseModel
            };
            StubBuilder.RequestBuilderFinisher.Clear();
            StubBuilder.ResponseBuilder.Clear();
            return stubRegistration ;
        }

        public ResponseBuilderFinisher(StubBuilder stubStubBuilder, HttpResponseModel model) : base(stubStubBuilder, model)
        {
            
        }
    }
}