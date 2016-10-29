namespace EasyStub.Common
{
    public static class StringExtensions
    {
        public static string ReplaceVal(this string target,string oldValue, string newValue)
        {
           return target.Replace(oldValue + "", newValue);
        }
    }
}
