using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Latsos.Shared;
using Newtonsoft.Json;
using RestSharp;
using Method = RestSharp.Method;
using RestRequest = RestSharp.RestRequest;

namespace Latsos.Client
{
    public class StubClient
    {
       

        private readonly RestClient _client;
        const string StubsResource = "Stubs";

        public StubClient(string server = "http://localhost/Latsos")
        {
            _client = new RestClient() {BaseUrl = new Uri(server)};
        }

        private IRestResponse Execute(IRestRequest request)
        {
            var response = _client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new StubException(response.ErrorMessage);
            }
            return response;
        }
        public void Add(StubRegistration stubRegistration)
        {
            var request = new RestRequestEx(StubsResource, Method.POST);
            request.AddJsonBody(stubRegistration);
            Execute(request);
        }

        public StubRegistration[] List()
        {
            var request = new RestRequestEx(StubsResource, Method.GET);
            var response = Execute(request);

            return JsonConvert.DeserializeObject<StubRegistration[]>(response.Content);
        }

        public void Clear()
        {
            var request = new RestRequestEx(StubsResource, Method.DELETE);
            Execute(request);
        }
    }
}