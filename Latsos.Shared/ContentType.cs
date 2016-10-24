using System.Text;

namespace Latsos.Shared
{
    

    public class ContentType
    {
        public ContentType(string mediaType, string charSet)
        {
            MediaType = mediaType;
            CharSet = charSet;
        }

        public ContentType()
        {
        }

        protected bool Equals(ContentType other)
        {
            return string.Equals(MediaType, other.MediaType) && Equals(CharSet, other.CharSet);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ContentType) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((MediaType?.GetHashCode() ?? 0)*397) ^ CharSet.GetHashCode();
            }
        }

        public string MediaType { get; set; }
        public string CharSet { set; get; }

        public override string ToString()
        {
            return $"MediaType: {MediaType}, CharSet: {CharSet}";
        }
    }
}