using System.Collections.Generic;
using System.Linq;

namespace EasyStub.Common
{
    public class DictionaryComparer 
    {
        
        public static bool CheckEquality<TKey, TValue>(Dictionary<TKey, TValue> dict1, Dictionary<TKey, TValue> dict2)
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
        public static bool CheckEquality<TKey, TValue,T>(Dictionary<TKey, TValue> dict1, Dictionary<TKey, TValue> dict2) where TValue:IEnumerable<T>
        {
            if (dict1 == dict2) return true;
            if ((dict1 == null) || (dict2 == null)) return false;
            if (dict1.Count != dict2.Count) return false;

            
            foreach (var kvp in dict1)
            {
                TValue value2;
                if (!dict2.TryGetValue(kvp.Key, out value2)) return false;
                if (!kvp.Value.SequenceEqual(value2)) return false;
            }
            return true;
        }
    }

    
}
