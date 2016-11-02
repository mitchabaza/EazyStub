using System.Collections.Generic;
using System.Linq;

namespace EasyStub.Common
{
    public class DictionaryComparer 
    {
        
        public static bool CheckEquality<TValue>(Dictionary<string, TValue> dict1, Dictionary<string, TValue> dict2)
        {
            if (dict1 == dict2) return true;
            if ((dict1 == null) || (dict2 == null)) return false;
            if (dict1.Count != dict2.Count) return false;

            var valueComparer = EqualityComparer<TValue>.Default;
            
            foreach (var kvp in dict1)
            {
                TValue value2;
                if (!dict2.TryGetValue(kvp.Key, out value2)) return false;
                if (!valueComparer.Equals(kvp.Value, value2)) return false;
            }
            return true;
        }
     }

    
}
