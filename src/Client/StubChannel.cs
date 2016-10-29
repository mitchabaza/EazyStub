using System;
using System.Net;
using EasyStub.Common;
using EasyStub.Common.Request;
using Newtonsoft.Json;
using RestSharp;
using Method = RestSharp.Method; 

namespace EasyStub.Client
{
    public class StubChannel
    {
        private readonly RestClient _client;
        const string StubsResource = "Registrations";
         
        public StubChannel(string server)
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

        internal IRestResponse Send(RequestRegistrationModel requestRegistrationModel)
        {
            var request = new RestRequestEx(requestRegistrationModel.LocalPath,
                ConvertMethod(requestRegistrationModel.Method.Value));
            if (requestRegistrationModel.Headers?.Value != null)
                foreach (var header in requestRegistrationModel.Headers.Value.Dictionary)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            request.RequestFormat = DataFormat.Json;
            request.AddBody(requestRegistrationModel.Body);
            return Execute(request);
        }

        private Method ConvertMethod(Common.Request.Method method)
        {
            if (method.Equals(Common.Request.Method.Delete))
            {
                return Method.DELETE;
            }
            if (method.Equals(Common.Request.Method.Post))
            {
                return Method.POST;
            }
            if (method.Equals(Common.Request.Method.Get))
            {
                return Method.GET;
            }
            if (method.Equals(Common.Request.Method.Options))
            {
                return Method.OPTIONS;
            }
            if (method.Equals(Common.Request.Method.Put))
            {
                return Method.PUT;
            }
            throw new ArgumentException();
        }

        public StubRegistration[] List()
        {
            var request = new RestRequestEx(StubsResource, Method.GET);
            var response = Execute(request);
            
            return JsonConvert.DeserializeObject<StubRegistration[]>(response.Content, new JsonSerializerSettings() {ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor});
        }

        
        private IRestResponse Execute(IRestRequest request)
        {
            var response = _client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new StubException(response.Content);
            }
            return response;
        }

        public void Reset()
        {
            var request = new RestRequestEx(StubsResource, Method.DELETE);
            Execute(request);
        }
    }
}