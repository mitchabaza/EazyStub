using Latsos.Shared;

namespace Latsos.Core
{
    public interface IBehaviorRepository
    {
        void Register(BehaviorRegistrationRequest request);
        HttpResponseModel Find(RequestRegistration requestRegistration);
        HttpResponseModel Unregister(RequestRegistration requestRegistration);
        RequestRegistration[] FindByLocalPath(string localPath);
        void RemoveAll();
        BehaviorRegistrationRequest [] GetAll();
    }
}