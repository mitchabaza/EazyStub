using System;

namespace Latsos.Shared.Request
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
            if (string.IsNullOrWhiteSpace(localPath))
            {
                throw new ArgumentException("localPath");
            }
            if (port == 0)
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

       

        public Body Body { get; }
        public Method Method { get; }
        public Headers Headers { get; }
        public string Query { get; }
        public string LocalPath { get; }
        public int Port { get; }

        public override string ToString()
        {
            return $"Body: {Body}, Method: {Method}, Headers: {Headers}, Query: {Query}, LocalPath: {LocalPath}, Port: {Port}";
        }
    }
}