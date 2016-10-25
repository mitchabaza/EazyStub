using System.Net.Http;
using Latsos.Shared;
using Latsos.Shared.Request;

namespace Latsos.Core
{
    public interface IRequestModelProcessor
    {
        HttpRequestModel Execute(HttpRequestModel model);
    }
}