using System;
using System.Linq;

namespace Latsos.Shared
{
    public class Body:IEquatable<Body>
    {
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Body) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Type?.GetHashCode() ?? 0)*397) ^ (Data?.GetHashCode() ?? 0);
            }
        }

        public string Type { get; set; }
        public byte[] Data { get; set; }
       
        public bool Equals(Body other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return this.Data.SequenceEqual(other.Data) && this.Type.Equals(other.Type);
        }
    }
}