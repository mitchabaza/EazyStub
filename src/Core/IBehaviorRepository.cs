using EasyStub.Common;
using EasyStub.Common.Request;
using EasyStub.Common.Response;

namespace EasyStub.Core
{
    public interface IBehaviorRepository
    {
        void Register(StubRegistration request);
        HttpResponseModel Find(RequestRegistrationModel requestRegistrationModel);
        HttpResponseModel Unregister(RequestRegistrationModel requestRegistrationModel);
        RequestRegistrationModel[] FindByLocalPath(string localPath);
        void RemoveAll();
        StubRegistration [] GetAll();
        HttpResponseModel Get(RequestRegistrationModel matchingRequest);
        void Unregister(int id);
    }
}