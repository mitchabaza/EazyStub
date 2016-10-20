using System;
using System.Net.Http;

namespace Latsos.Shared
{
    public class Method:IEquatable<Method>
    {
    
        private static readonly Method GetMethod = new Method("GET");
        private static readonly Method PutMethod = new Method("PUT");
        private static readonly Method PostMethod = new Method("POST");
        private static readonly Method DeleteMethod = new Method("DELETE");
        private static readonly Method HeadMethod = new Method("HEAD");
        private static readonly Method OptionsMethod = new Method("OPTIONS");
        private static readonly Method TraceMethod = new Method("TRACE");
        private readonly string _method;

        public string Verb => _method;


        public static implicit operator Method(HttpMethod method)
        {
            return new Method(method.ToString());
        }
        public Method(string verb)
        {
            this._method = verb;
        }
        public override int GetHashCode()
        {
            return this._method.ToUpperInvariant().GetHashCode();
        }
        public override string ToString()
        {
            return this._method;
        }
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Method);
        }
        public bool Equals(Method other)
        {
            if (other == null)
                return false;
            if (this._method == other._method)
                return true;
            return string.Compare(this._method, other._method, StringComparison.OrdinalIgnoreCase) == 0;
        }

      
        public static Method Get => Method.GetMethod;
        public static Method Put => Method.PutMethod;
        public static Method Post => Method.PostMethod;
        public static Method Delete => Method.DeleteMethod;
        public static Method Trace => Method.TraceMethod;
        public static Method Head => Method.HeadMethod;
        public static Method Options=> Method.OptionsMethod;
    }
}