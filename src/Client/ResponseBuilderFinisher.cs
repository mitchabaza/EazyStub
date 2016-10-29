using EasyStub.Common;
using EasyStub.Common.Response;

namespace EasyStub.Client
{
    public class ResponseBuilderFinisher  : ResponseBuilder
    {
       

        public new StubRegistration Build()
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
        public  StubRegistration BuildAndRegister()
        {

            var stubRegistration = new StubRegistration()
            {
                Request = StubBuilder.RequestBuilderFinisher.Build(),
                Response = ResponseModel
            };
            StubBuilder.RequestBuilderFinisher.Clear();
            StubBuilder.ResponseBuilder.Clear();
            StubBuilder.Channel.Register(stubRegistration);
            return stubRegistration;
        }
        public ResponseBuilderFinisher(StubBuilder stubStubBuilder, HttpResponseModel model) : base(stubStubBuilder, model)
        {
            
        }
    }
}