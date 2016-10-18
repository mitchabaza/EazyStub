using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Latsos.Shared;

namespace Latsos.Core
{
    public class InMemoryBehaviorRepository : IBehaviorRepository
    {
        private readonly ConcurrentDictionary<RequestRegistration, BehaviorRegistrationRequest> _registeredRequests =
            new ConcurrentDictionary<RequestRegistration, BehaviorRegistrationRequest>(new EqualityCompaper());
      

        public void Register(BehaviorRegistrationRequest request)
        {
            _registeredRequests.TryAdd(request.RequestRegistration, request);
        }

        public HttpResponseModel Find(RequestRegistration requestRegistration)
        {
            
            return _registeredRequests[requestRegistration]?.ResponseModel;
        }
        public RequestRegistration[] FindByLocalPath(string localPath)
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

        public HttpResponseModel Unregister(RequestRegistration requestRegistration)
        {
            BehaviorRegistrationRequest outValue;
            _registeredRequests.TryRemove(requestRegistration, out outValue);
            return outValue.ResponseModel;
        }
    }

    internal class EqualityCompaper : IEqualityComparer<RequestRegistration>
    {
       

        public bool Equals(RequestRegistration x, RequestRegistration y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(RequestRegistration obj)
        {
           return  obj.GetHashCode();
        }
    }
}