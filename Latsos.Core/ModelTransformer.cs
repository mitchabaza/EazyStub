using System.Net.Http;
using System.Text;
using Latsos.Shared;

namespace Latsos.Core
{
    public class ModelTransformer : IModelTransformer
    {
        public HttpResponseMessage Transform(HttpResponseModel responseModel)
        {
             var httpResponseMessage = new HttpResponseMessage(responseModel.StatusCode) {Content = new StringContent(responseModel.Body.Data,Encoding.Default,responseModel.Body.ContentType)  };
            foreach (var header in responseModel.Headers.Dictionary)
            {
                httpResponseMessage.Headers.Add(header.Key, header.Value);
            }
            return httpResponseMessage;
        }

        public HttpRequestModel Transform(HttpRequestMessage request)
        {
            var body = new Body()
            {
                Data = request.Content.ReadAsStringAsync().Result,
                ContentType = request.Content.Headers?.ContentType?.MediaType
            };
            var method = request.Method;
            var query = request.RequestUri.Query;
            var port = request.RequestUri.Port;
            var headers = new Headers();
            foreach (var httpRequestHeader in request.Headers)
            {
                headers.Add(httpRequestHeader.Key, string.Join(",", httpRequestHeader.Value));
            }
            var localPath = request.RequestUri.LocalPath;
            return new HttpRequestModel(body, method, headers, query, localPath, port);
        }
    }
}