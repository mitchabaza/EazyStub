using System;
using System.Net.Http;
using Latsos.Shared;

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


        public void Execute(HttpRequestModel model)
        {
            
            var localPath = model.LocalPath;

            if (string.IsNullOrWhiteSpace(localPath))
            {
                return;
            }
            var firstSegment = localPath.IndexOf(ReplacementText, 0, StringComparison.Ordinal);

            if (firstSegment >= 0)
            {
                model.LocalPath = localPath.Substring(ReplacementText.Length);
            }
        }
    }
}