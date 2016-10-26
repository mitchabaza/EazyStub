using Latsos.Shared;

namespace Latsos.Client
{
    public static class Extensions
    {
        public static void Register(this StubRegistration registration)
        {
            var s = new Factory();
            s.Register(registration);
        }
        public static void Unregister(this StubRegistration registration)
        {
            var s = new Factory();
            s.UnRegister(registration);
        }
    }
}