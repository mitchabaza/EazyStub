using System;
using System.Net.Http;
using Latsos.Shared;
using Latsos.Shared.Request;

namespace Latsos.Core
{
    public class LocalPathProcessor: IRequestModelProcessor
    {
        private readonly IHostingEnvironment _environment;

        private string ReplacementText => $@"{_environment.ApplicationVirtualPath}";

        public LocalPathProcessor(IHostingEnvironment environment)
        {
            _environment = environment;
        }


        public HttpRequestModel Execute(HttpRequestModel model)
        {
            
            var localPath = model.LocalPath;

            if (string.IsNullOrWhiteSpace(localPath))
            {
                return model;
            }
            var firstSegment = localPath.IndexOf(ReplacementText, 0, StringComparison.Ordinal);

            if (firstSegment >= 0)
            {
                return new HttpRequestModel(model.Body, model.Method,model.Headers,model.Query, localPath.Substring(ReplacementText.Length),model.Port);
            }
            return model;
        }
    }
}