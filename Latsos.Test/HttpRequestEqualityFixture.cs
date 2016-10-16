using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Text;
using FluentAssertions;
using Latsos.Shared;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Latsos.Test
{
    [TestFixture]
    public class HttpRequestEqualityFixture
    {
        private readonly Fixture _fixture = new Fixture();

        private class Value : IEquatable<Value>
        {
            public string Property { get; set; }

            public bool Equals(Value other)
            {
                if (other.Property.Equals(this.Property))
                {
                    return true;
                }
                return false;
            }
        }


        [TestCase(true,true,true,true,true,   "path", "path", true )]
        [TestCase(false, true, true, true, true, "path", "path", false)]
        [TestCase(true, false, true, true, true, "path", "path", false)]
        [TestCase(true, true, false, true, true, "path", "path", false)]
        [TestCase(true, true, true, false, true, "path", "path", false)]
        [TestCase(true, true, true, true, false, "path", "path", false)]
        [TestCase(true, true, true, true, true, "path", "LocalPath", false)]

        public void HttpRequest_Equals_ShouldWork(bool port1,  bool queryString1, 
            bool headers1,   bool content1,  bool method1,  string path1, string path2, bool shoulBeEqual )
        {

            var request1 = new HttpRequestRegistration();
            var request2= new HttpRequestRegistration();

            request1.Port = CreateMock<int>(port1);
            request2.Port = CreateMock<int>(port1);
            
            request1.Query = CreateMock<string>(queryString1);
            request2.Query = CreateMock<string>(queryString1);

            request1.Headers= CreateMock<Headers>(headers1);
            request2.Headers = CreateMock<Headers>(headers1);

            request1.Method = CreateMock<HttpMethod>(method1);
            request2.Method = CreateMock<HttpMethod>(method1);

            request1.Body = CreateMock<Body2>(content1);
            request2.Body= CreateMock<Body2>(content1);
            
            request1.LocalPath = path1;
            request2.LocalPath = path2;

            if (shoulBeEqual)
            {
                request1.ShouldEqual(request2);

            }
            else
            {
                request1.ShouldNotEqual(request2);
            }
        }

        private MatchRule<T> CreateMock<T>(bool returnVal) where T: IEquatable<T>
        {
            var fixture = new Fixture( );
            fixture.Customize(new CompositeCustomization(new AutoMoqCustomization()));
            var mock = fixture.Create<Mock<MatchRule<T>>>();
            mock.Setup(s => s.Equals( It.IsAny<MatchRule<T>>())).Returns(returnVal);

            return mock.Object;
        }
        [Test]
        public void Header_Equals_ShouldWork()
        {

         
            var headers1 = new Headers();
            headers1.Add("id", "b");
            var headers2 = new Headers();
            headers2.Add("id", "b");
            headers2.ShouldEqual(headers1);

            headers1 = new Headers();
            headers1.Add("id", "asdasd");
            headers2 = new Headers();
            headers2.Add("id", "buzz");
            headers2.ShouldNotEqual(headers1);

            headers1 = new Headers();
            headers1.Add("id", null);
            headers2 = new Headers();
            headers2.Add("id", "");
            headers2.ShouldNotEqual(headers1);

        }

        [Test]
        public void Method_Equals_ShouldWork()
        {

            var method1 = HttpMethod.Delete;
            var method2 = HttpMethod.Delete;

            method1.ShouldEqual(method2);

             method1 = HttpMethod.Delete;
            method2 = HttpMethod.Get;

            method1.ShouldNotEqual(method2);

        }

    
        [Test]
        public void MatchRule_Equals_ShouldReturnFalse_WhenAnyOrValueDifferent()
        {
            var matchRule1 = new MatchRule<Value>(false, new Value() {Property = "A"});
            var matchRule2 = new MatchRule<Value>(true, new Value() {Property = "A"});
            matchRule2.ShouldNotEqual(matchRule1);

             matchRule1 = new MatchRule<Value>(true, new Value() { Property = "A" });
            matchRule2 = new MatchRule<Value>(true, new Value() { Property = "B" });
            matchRule2.ShouldNotEqual(matchRule1);
        
        }

        [Test]
        public void MatchRule_Equals_ShouldReturnTrue_WhenAnyAndValueSame()
        {
            var matchRule1 = new MatchRule<Value>(true, new Value() { Property = "A" });
            var matchRule2 = new MatchRule<Value>(true, new Value() { Property = "A" });
            matchRule2.ShouldEqual(matchRule1);

            matchRule1 = new MatchRule<Value>(false, new Value() { Property = "Z" });
            matchRule2 = new MatchRule<Value>(false, new Value() { Property = "Z" });
            matchRule2.ShouldEqual(matchRule1);

        }

        [Test]
        public void MatchRule_Equals_ShouldReturnTrue_WhenAnySameAndValueNull()
        {
            var matchRule1 = new MatchRule<Value>(true, null);
            var matchRule2 = new MatchRule<Value>(true, null);
            matchRule2.ShouldEqual(matchRule1);

            matchRule1 = new MatchRule<Value>(false, null);
            matchRule2 = new MatchRule<Value>(true, null);
            matchRule2.ShouldNotEqual(matchRule1);

        }

        [TestCase("content", "application-java", "content", "application-java", true)]
        [TestCase("content", "application-java", "content1", "application-java", false)]
        [TestCase("content", "application-java", "content", "application-html", false)]
        public void Contents_Equals_ShouldWork(string data1, string type1, string data2, string type2, bool equal )
        {
           var contents1 = new Body() {Data =Encoding.ASCII.GetBytes(data1), Type = type1};
           var contents2 = new Body() { Data = Encoding.ASCII.GetBytes(data2), Type = type2 };

            if (equal)
            {
                contents1.ShouldEqual(contents2);
            }
            else
            {
                contents1.ShouldNotEqual(contents2);
            }

        }
        [Test]
        public void Dictionary_ShouldUseEquals()
        {
            
            var dictionary = new ConcurrentDictionary<HttpRequestRegistration, string>();

            var httpRequest1 = _fixture.Create<HttpRequestRegistration>();
            var httpRequest2 = _fixture.Create<HttpRequestRegistration>();

            dictionary.TryAdd(httpRequest1,"").Should().BeTrue();
            dictionary.TryAdd(httpRequest2,"").Should().BeTrue();


            var dictionary2 = new ConcurrentDictionary<HttpRequestRegistration, string>();

            var httpRequest3 = _fixture.Create<HttpRequestRegistration>();
            var httpRequest4 = httpRequest3.Clone();

            dictionary2.TryAdd(httpRequest3, "").Should().BeTrue();
            dictionary2.TryAdd(httpRequest4, "").Should().BeFalse();


        }
    }
}