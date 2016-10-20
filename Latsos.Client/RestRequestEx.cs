using System;
using RestSharp;
using RestSharp.Newtonsoft.Json;
using RestRequest = RestSharp.RestRequest;

namespace Latsos.Client
{
    public class RestRequestEx : RestRequest
    {

        public RestRequestEx()
        {
            base.JsonSerializer = new NewtonsoftJsonSerializer();
        }

        public RestRequestEx(Method method) : base(method)
        {
            base.JsonSerializer = new NewtonsoftJsonSerializer();
        }

        public RestRequestEx(string resource) : base(resource)
        {
            base.JsonSerializer = new NewtonsoftJsonSerializer();
        }

        public RestRequestEx(string resource, Method method) : base(resource, method)
        {
            base.JsonSerializer = new NewtonsoftJsonSerializer();
        }

        public RestRequestEx(Uri resource) : base(resource)
        {
            base.JsonSerializer = new NewtonsoftJsonSerializer();
        }

        public RestRequestEx(Uri resource, Method method) : base(resource, method)
        {
            base.JsonSerializer = new NewtonsoftJsonSerializer();
        }
    }
}