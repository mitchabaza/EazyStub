using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Latsos.Core;
using Latsos.Shared;
using Latsos.Shared.Request;
using Newtonsoft.Json;
using RestSharp;
using Method = RestSharp.Method; 

namespace Latsos.Client
{
    public class Factory
    {
        private readonly RestClient _client;
        const string StubsResource = "Stubs";
        private StubBuilder _builder = new StubBuilder();
        

        public Factory(string server = "http://localhost/Latsos")
        {
            _client = new RestClient() {BaseUrl = new Uri(server)};
        }


        public void Register(StubRegistration stubRegistration)
        {
            var request = new RestRequestEx(StubsResource, Method.POST);
            request.AddJsonBody(stubRegistration);
            Execute(request);
        }

        public void UnRegister(StubRegistration stubRegistration)
        {
            var request = new RestRequestEx(StubsResource + "/" + stubRegistration.GetHashCode().ToString(), Method.DELETE);
            Execute(request);
        }

        public IRestResponse Send(RequestRegistration requestRegistration)
        {
            var request = new RestRequestEx(requestRegistration.LocalPath,
                ConvertMethod(requestRegistration.Method.Value));
            if (requestRegistration.Headers?.Value != null)
                foreach (var header in requestRegistration.Headers.Value.Dictionary)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            request.RequestFormat = DataFormat.Json;
            request.AddBody(requestRegistration.Body);
            return Execute(request);
        }

        private Method ConvertMethod(Shared.Request.Method method)
        {
            if (method.Equals(Shared.Request.Method.Delete))
            {
                return RestSharp.Method.DELETE;
            }
            if (method.Equals(Shared.Request.Method.Post))
            {
                return RestSharp.Method.POST;
            }
            if (method.Equals(Shared.Request.Method.Get))
            {
                return RestSharp.Method.GET;
            }
            if (method.Equals(Shared.Request.Method.Options))
            {
                return RestSharp.Method.OPTIONS;
            }
            if (method.Equals(Shared.Request.Method.Put))
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

        private IRestResponse Execute(IRestRequest request)
        {
            var response = _client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new StubException(response.ErrorMessage);
            }
            return response;
        }
    }
}