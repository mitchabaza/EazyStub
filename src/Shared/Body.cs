using System;

namespace EasyStub.Common
{
    public class Body:IEquatable<Body>
    {
        public Body()
        {
            ContentType = new ContentType();
        }

        public Body(ContentType contentType, string data)
        {
            ContentType = contentType;
            Data = data;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Body) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((ContentType?.GetHashCode() ?? 0)*397) ^ (Data?.GetHashCode() ?? 0);
            }
        }

        
        public ContentType ContentType { get; set; }
        public string Data { get; set; }
      
        public bool Equals(Body other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Data.Equals(other.Data) && ContentType.Equals(other.ContentType);
        }

        public override string ToString()
        {
            return $"Type: {ContentType}, Data: {Data}";
        }
    }
}
