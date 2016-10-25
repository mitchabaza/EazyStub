using Latsos.Shared;
using Latsos.Shared.Request;
using Latsos.Shared.Response;

namespace Latsos.Core
{
    public interface IBehaviorRepository
    {
        void Register(StubRegistration request);
        HttpResponseModel Find(RequestRegistration requestRegistration);
        HttpResponseModel Unregister(RequestRegistration requestRegistration);
        RequestRegistration[] FindByLocalPath(string localPath);
        void RemoveAll();
        StubRegistration [] GetAll();
    }
}