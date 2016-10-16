using Latsos.Shared;

namespace Latsos.Core
{
    public interface IBehaviorRepository
    {
        void Add(BehaviorRegistrationRequest request);
        StubHttpResponse Find(HttpRequestRegistration requestRegistration);
        StubHttpResponse Remove(HttpRequestRegistration requestRegistration);
        HttpRequestRegistration[] FindByLocalPath(string localPath);
        void RemoveAll();
        BehaviorRegistrationRequest [] GetAll();
    }
}