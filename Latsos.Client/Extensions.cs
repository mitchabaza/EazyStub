using Latsos.Shared;
using Newtonsoft.Json;

namespace Latsos.Client
{
    public static class Extensions
    {
        public static void Register(this StubRegistration registration)
        {
            var s = new Factory();
            s.Register(registration);
        }
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public static void Unregister(this StubRegistration registration)
        {
            var s = new Factory();
            s.UnRegister(registration);
        }
    }
}