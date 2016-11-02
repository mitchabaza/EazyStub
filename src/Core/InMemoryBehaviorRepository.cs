using System.Collections.Concurrent;
using System.Linq;
using EasyStub.Common;
using EasyStub.Common.Request;
using EasyStub.Common.Response;

namespace EasyStub.Core
{
    public class InMemoryBehaviorRepository : IBehaviorRepository
    {
        private readonly ConcurrentDictionary<int, StubRegistration> _registeredRequests =
            new ConcurrentDictionary<int, StubRegistration> (  );
      

        public bool Register(StubRegistration request)
        {
            if (_registeredRequests.TryAdd(request.Request.GetHashCode(), request))
            {
                return true;
                 
            }
            return false;
        }

        public HttpResponseModel Find(RequestRegistrationModel requestRegistrationModel)
        {
            
            return _registeredRequests[requestRegistrationModel.GetHashCode()]?.Response;
        }
        public RequestRegistrationModel[] FindByLocalPath(string localPath)
        {
          return  _registeredRequests.Values.Where(k =>  k.Request.LocalPath.Equals(localPath)).Select(s => s.Request).ToArray();

        }

        public void RemoveAll()
        {
            _registeredRequests.Clear();
        }

        public StubRegistration[] GetAll()
        {
            return _registeredRequests.Values.ToArray();
        }

        public HttpResponseModel Get(RequestRegistrationModel matchingRequest)
        {
            return _registeredRequests[matchingRequest.GetHashCode()].Response;
        }

        public HttpResponseModel Unregister(RequestRegistrationModel requestRegistrationModel)
        {
            StubRegistration outValue;
            _registeredRequests.TryRemove(requestRegistrationModel.GetHashCode(), out outValue);
            return outValue.Response;
        }
        public void Unregister(int id)
        {
            StubRegistration outValue;
            _registeredRequests.TryRemove(id, out outValue);
        }
    }


}