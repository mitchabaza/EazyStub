using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Latsos.Test
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
            return f;
        }
    }
}