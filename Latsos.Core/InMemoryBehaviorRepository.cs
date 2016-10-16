using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Latsos.Shared;

namespace Latsos.Core
{
    public class InMemoryBehaviorRepository : IBehaviorRepository
    {
        private readonly ConcurrentDictionary<HttpRequestRegistration, BehaviorRegistrationRequest> _registeredRequests =
            new ConcurrentDictionary<HttpRequestRegistration, BehaviorRegistrationRequest>(new EqualityCompaper());
      

        public void Add(BehaviorRegistrationRequest request)
        {
            _registeredRequests.TryAdd(request.RequestRegistration, request);
        }

        public StubHttpResponse Find(HttpRequestRegistration requestRegistration)
        {
            
            return _registeredRequests[requestRegistration]?.Response;
        }
        public HttpRequestRegistration[] FindByLocalPath(string localPath)
        {
          return  _registeredRequests.Keys.Where(k => k.LocalPath.Equals(localPath)).Select(s => s).ToArray();

        }

        public void RemoveAll()
        {
            _registeredRequests.Clear();
        }

        public BehaviorRegistrationRequest[] GetAll()
        {
            return _registeredRequests.Values.ToArray();
        }

        public StubHttpResponse Remove(HttpRequestRegistration requestRegistration)
        {
            BehaviorRegistrationRequest outValue;
            _registeredRequests.TryRemove(requestRegistration, out outValue);
            return outValue.Response;
        }
    }

    internal class EqualityCompaper : IEqualityComparer<HttpRequestRegistration>
    {
       

        public bool Equals(HttpRequestRegistration x, HttpRequestRegistration y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(HttpRequestRegistration obj)
        {
           return  obj.GetHashCode();
        }
    }
}