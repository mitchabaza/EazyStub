using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Latsos.Shared;
using Latsos.Shared.Request;
using Latsos.Shared.Response;

namespace Latsos.Core
{
    public class InMemoryBehaviorRepository : IBehaviorRepository
    {
        private readonly ConcurrentDictionary<int, StubRegistration> _registeredRequests =
            new ConcurrentDictionary<int, StubRegistration> (  );
      

        public void Register(StubRegistration request)
        {

            _registeredRequests.TryAdd(request.Request.GetHashCode(), request);
        }

        public HttpResponseModel Find(RequestRegistration requestRegistration)
        {
            
            return _registeredRequests[requestRegistration.GetHashCode()]?.Response;
        }
        public RequestRegistration[] FindByLocalPath(string localPath)
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

        public HttpResponseModel Get(RequestRegistration matchingRequest)
        {
            return _registeredRequests[matchingRequest.GetHashCode()].Response;
        }

        public HttpResponseModel Unregister(RequestRegistration requestRegistration)
        {
            StubRegistration outValue;
            _registeredRequests.TryRemove(requestRegistration.GetHashCode(), out outValue);
            return outValue.Response;
        }
        public void Unregister(int id)
        {
            StubRegistration outValue;
            _registeredRequests.TryRemove(id, out outValue);
        }
    }


}