using System;
using RestSharp;
using RestSharp.Newtonsoft.Json;
using RestRequest = RestSharp.RestRequest;

namespace EasyStub.Client
{
    public class RestRequestEx : RestRequest
    {

        public RestRequestEx()
        {
            JsonSerializer = new NewtonsoftJsonSerializer();
        }

        public RestRequestEx(Method method) : base(method)
        {
            JsonSerializer = new NewtonsoftJsonSerializer();
        }

        public RestRequestEx(string resource) : base(resource)
        {
            JsonSerializer = new NewtonsoftJsonSerializer();
        }

        public RestRequestEx(string resource, Method method) : base(resource, method)
        {
            JsonSerializer = new NewtonsoftJsonSerializer();
        }

        public RestRequestEx(Uri resource) : base(resource)
        {
            JsonSerializer = new NewtonsoftJsonSerializer();
        }

        public RestRequestEx(Uri resource, Method method) : base(resource, method)
        {
            JsonSerializer = new NewtonsoftJsonSerializer();
        }
    }
}