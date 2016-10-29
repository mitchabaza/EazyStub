using EasyStub.Common;
using EasyStub.Common.Request;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace EasyStub.Test
{
    public abstract class FixtureBase<T> where T:class
    {
        private T _sut;

        protected T Sut => _sut ?? (_sut = Fixture.Create<T>());


        protected Fixture Fixture = new Fixture();
        [SetUp]
        public void SetUp()
        {
            Fixture = CreateFixture();
            _sut = null;
            OnFixtureSetup();

        }

        protected virtual void OnFixtureSetup()
        {
            
        }

        private static Fixture CreateFixture()
        {
            var f = new Fixture();
            f.Customize(new CompositeCustomization(new AutoMoqCustomization()));
            f.Register(()=>new HttpRequestModel(f.Create<Body>(), f.Create<Method>(), f.Create<Headers>(), f.Create<string>(), "/" + f.Create<string>(),114));
            return f;
        }
    }
}