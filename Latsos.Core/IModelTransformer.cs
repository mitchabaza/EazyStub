using System.Net.Http;
using Latsos.Shared;
using Latsos.Shared.Request;
using Latsos.Shared.Response;

namespace Latsos.Core
{
    public interface IModelTransformer
    {
        HttpResponseMessage Transform(HttpResponseModel responseModel);
        HttpRequestModel Transform(HttpRequestMessage request);
    }
}