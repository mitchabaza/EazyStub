using System.Net.Http;
using EasyStub.Common.Request;
using EasyStub.Common.Response;

namespace EasyStub.Core
{
    public interface IModelTransformer
    {
        HttpResponseMessage Transform(HttpResponseModel responseModel);
        HttpRequestModel Transform(HttpRequestMessage request);
    }
}