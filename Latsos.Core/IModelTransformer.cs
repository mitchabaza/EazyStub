using System.Net.Http;
using Latsos.Shared;

namespace Latsos.Core
{
    public interface IModelTransformer
    {
        HttpResponseMessage Transform(HttpResponseModel responseModel);
        HttpRequestModel Transform(HttpRequestMessage request);
    }
}