using System.Net.Http;
using System.Text;
using Latsos.Shared;
using Latsos.Shared.Request;
using Latsos.Shared.Response;

namespace Latsos.Core
{
    public class ModelTransformer : IModelTransformer
    {
        private readonly IRequestModelProcessor _processor;

        public ModelTransformer(IRequestModelProcessor processor)
        {
            _processor = processor;
        }

        public HttpResponseMessage Transform(HttpResponseModel responseModel)
        {
            var httpResponseMessage = new HttpResponseMessage(responseModel.StatusCode);
            if (responseModel.Body.Data != null)
            {
                if (responseModel.Body.ContentType?.MediaType != null && responseModel.Body.ContentType?.CharSet != null)
                {
                    httpResponseMessage.Content = new StringContent(responseModel.Body.Data,
                        Encoding.GetEncoding(responseModel.Body.ContentType.CharSet),
                        responseModel.Body.ContentType.MediaType);
                }
                else
                {
                    httpResponseMessage.Content = new StringContent(responseModel.Body.Data);
                }
            }

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
                ContentType = new ContentType() {MediaType = request.Content.Headers?.ContentType?.MediaType}
            };
            if (request.Content?.Headers?.ContentType?.CharSet != null)
            {
                body.ContentType.CharSet = request.Content.Headers.ContentType.CharSet;
            }
            var method = request.Method;
            var query = request.RequestUri.Query;
            var port = request.RequestUri.Port;
            var headers = new Headers();
            foreach (var httpRequestHeader in request.Headers)
            {
                headers.Add(httpRequestHeader.Key, string.Join(",", httpRequestHeader.Value));
            }
            var localPath = request.RequestUri.LocalPath;
            var requestModel = new HttpRequestModel(body, method, headers, query, localPath, port);
            return _processor.Execute(requestModel);
        }
    }
}