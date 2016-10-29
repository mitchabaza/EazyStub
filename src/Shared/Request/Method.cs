using System;
using System.Net.Http;

namespace EasyStub.Common.Request
{
    public class Method:IEquatable<Method>
    {
        // private static readonly Method PatchMethod = new Method("PATCH");

        public string Verb { get; }


        public static implicit operator Method(HttpMethod method)
        {
            return new Method(method.ToString());
        }
        public Method(string verb)
        {
            Verb = verb;
        }
        public override int GetHashCode()
        {
            return Verb.ToUpperInvariant().GetHashCode();
        }
        public override string ToString()
        {
            return Verb;
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as Method);
        }
        public bool Equals(Method other)
        {
            if (other == null)
                return false;
            if (Verb == other.Verb)
                return true;
            return string.Compare(Verb, other.Verb, StringComparison.OrdinalIgnoreCase) == 0;
        }

      
        public static Method Get { get; } = new Method("GET");

        public static Method Put { get; } = new Method("PUT");

        public static Method Post { get; } = new Method("POST");

        public static Method Delete { get; } = new Method("DELETE");

        public static Method Trace { get; } = new Method("TRACE");

        public static Method Head { get; } = new Method("HEAD");

        public static Method Options { get; } = new Method("OPTIONS");
    }
}