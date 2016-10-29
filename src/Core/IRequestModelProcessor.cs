using EasyStub.Common.Request;

namespace EasyStub.Core
{
    public interface IRequestModelProcessor
    {
        HttpRequestModel Execute(HttpRequestModel model);
    }
}