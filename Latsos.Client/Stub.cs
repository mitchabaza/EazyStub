using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Latsos.Shared;
using Newtonsoft.Json;
using RestSharp;

namespace Latsos.Client
{
    public class Stub
    {
        private readonly RestClient _client;

        public Stub()
        {
            _client = new RestClient() {BaseUrl = new Uri("http://localhost/Latsos") };

        }

        public void Register(BehaviorRegistrationRequest requestBehaviorRegistrationRequest)
        {
            var request = new RestRequest("Stub/Setup", Method.POST);
            request.AddJsonBody(requestBehaviorRegistrationRequest);
            _client.Execute(request);
        }
        public BehaviorRegistrationRequest Peek()
        {
            var request = new RestRequest("Stub/Peek", Method.GET);
            var response=_client.Execute(request);
            return JsonConvert.DeserializeObject<BehaviorRegistrationRequest>(response.Content);

        }
        public void ClearAll()
        {
            var request = new RestRequest("Stub/Clear", Method.POST);
            _client.Execute(request);
        }

    }
}