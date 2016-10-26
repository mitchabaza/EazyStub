using System.Linq;
using EnsureThat;

namespace Latsos.Shared
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