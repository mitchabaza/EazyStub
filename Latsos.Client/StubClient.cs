using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Latsos.Core;
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
        public IRestResponse Send(RequestRegistration requestRegistration)
        {
            
            var request = new RestRequestEx(requestRegistration.LocalPath, ConvertMethod(requestRegistration.Method.Value));
            if (requestRegistration.Headers?.Value != null)
                foreach (var header in requestRegistration.Headers.Value.Dictionary)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            request.RequestFormat= DataFormat.Json;
            request.AddBody(requestRegistration.Body);
            return Execute(request);
        }

        private Method ConvertMethod( Shared.Method method)
        {
            if (method.Equals(Shared.Method.Delete))
            {
                return RestSharp.Method.DELETE;
            }
            if (method.Equals(Shared.Method.Post))
            {
                return RestSharp.Method.POST;
            }
            if (method.Equals(Shared.Method.Get))
            {
                return RestSharp.Method.GET;
            }
            if (method.Equals(Shared.Method.Options))
            {
                return RestSharp.Method.OPTIONS;
            }
            if (method.Equals(Shared.Method.Put))
            {
                return RestSharp.Method.PUT;
            }
            throw new ArgumentException();
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