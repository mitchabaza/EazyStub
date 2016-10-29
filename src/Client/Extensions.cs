using System;
using EasyStub.Common;
using Newtonsoft.Json;

namespace EasyStub.Client
{
    public static class Extensions
    {
        private static void Validate()
        {
            if (string.IsNullOrWhiteSpace(Settings.Url))
            {
                throw new InvalidOperationException(
                    "You must first set the ServerUrl by calling Settings.SetServerUrl()");
            }
        }

        //public static void Register(this ResponseBuilderFinisher registration)
        //{
        //    Validate();
        //    var s = new StubChannel(Settings.Url);
        //    s.Register(registration.Build());
        //}

        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static void Unregister(this StubRegistration registration)
        {
            var s = new StubChannel(Settings.Url);
            s.UnRegister(registration);
        }
    }
}