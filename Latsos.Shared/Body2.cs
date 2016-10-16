using System;

namespace Latsos.Shared
{
    public class Body2 : IEquatable<Body2>
    {
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Body)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Type?.GetHashCode() ?? 0) * 397) ^ (Data?.GetHashCode() ?? 0);
            }
        }

        public string Type { get; set; }
        public string Data { get; set; }

        public bool Equals(Body2 other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return this.Data.Equals(other.Data) && this.Type.Equals(other.Type);
        }
    }
}