using System;
using System.Net;
using EasyStub.Common.Request;

namespace EasyStub.Common.Response
{
    /// <summary>
    /// The HTTP Response returned when a particular <see cref="RequestRegistrationModel"></see> is matched
    /// </summary>
    public class HttpResponseModel
    {
        public short? Wait { get; set; }
        public Body Body { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public Headers Headers { get; set; }

        public HttpResponseModel()
        {
            Headers = new Headers();
            Body=new Body();
            
        }

        public override string ToString()
        {
            return $"Body: {Body}, StatusCode: {StatusCode}, Headers: {Headers}";
        }
    }
}