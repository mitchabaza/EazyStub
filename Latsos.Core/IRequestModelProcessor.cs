using System.Net.Http;
using Latsos.Shared;

namespace Latsos.Core
{
    public interface IRequestModelProcessor
    {
        void Execute(HttpRequestModel model);
    }
}