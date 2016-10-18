using System;
using System.Net.Http;

namespace Latsos.Shared
{
    public class HttpRequestModel
    {
        public HttpRequestModel(Body body, Method method, Headers headers, string query, string localPath, int port)
        {
            if (body == null)
            {
                throw new ArgumentException("body");
            }
            if (headers == null)
            {
                throw new ArgumentException("headers");
            }
            if (method == null)
            {
                throw new ArgumentException("method");
            }
            if (query == null)
            {
                throw new ArgumentException("query");
            }
            if (localPath == null )
            {
                throw new ArgumentException("localPath");
            }
            if (port == 00)
            {
                throw new ArgumentException("port");
            }
            Body = body;
            Method = method;
            Headers = headers;
            Query = query;
            LocalPath = localPath;
            Port = port;
        }

       

        public Body Body { get; set; }
        public Method Method { get; set; }
        public Headers Headers { get; set; }
        public string Query { get; set; }
        public string LocalPath { get; set; }
        public int Port { get; set; }

        public override string ToString()
        {
            return $"Body: {Body}, Method: {Method}, Headers: {Headers}, Query: {Query}, LocalPath: {LocalPath}, Port: {Port}";
        }
    }
}