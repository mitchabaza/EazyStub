using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Latsos.Shared;

namespace Latsos.Core
{
    public class InMemoryBehaviorRepository : IBehaviorRepository
    {
        private readonly ConcurrentDictionary<RequestRegistration, StubRegistration> _registeredRequests =
            new ConcurrentDictionary<RequestRegistration, StubRegistration> (  );
      

        public void Register(StubRegistration request)
        {

            _registeredRequests.TryAdd(request.Request, request);
        }

        public HttpResponseModel Find(RequestRegistration requestRegistration)
        {
            
            return _registeredRequests[requestRegistration]?.Response;
        }
        public RequestRegistration[] FindByLocalPath(string localPath)
        {
          return  _registeredRequests.Keys.Where(k => k.LocalPath.Equals(localPath)).Select(s => s).ToArray();

        }

        public void RemoveAll()
        {
            _registeredRequests.Clear();
        }

        public StubRegistration[] GetAll()
        {
            return _registeredRequests.Values.ToArray();
        }

        public HttpResponseModel Unregister(RequestRegistration requestRegistration)
        {
            StubRegistration outValue;
            _registeredRequests.TryRemove(requestRegistration, out outValue);
            return outValue.Response;
        }
    }

    public class Comparer:IEqualityComparer<RequestRegistration>
    {
        public bool Equals(RequestRegistration x, RequestRegistration y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(RequestRegistration obj)
        {
            return obj.GetHashCode();
        }
    }
}