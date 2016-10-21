using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latsos.Shared
{
    public static class StringExtensions
    {
        public static string ReplaceVal(this string target,string oldValue, string newValue)
        {
           return target.Replace(oldValue + "", newValue);
        }
    }
}
