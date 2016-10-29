using EnsureThat;

namespace EasyStub.Common
{
    public class QueryString : KeyValuePairs
    {
        public override void Add(string key, string value)
        {
            Ensure.That(key).IsNotNullOrEmpty();
            Ensure.That(value).IsNotNullOrEmpty();

            
        }

        
    }
}