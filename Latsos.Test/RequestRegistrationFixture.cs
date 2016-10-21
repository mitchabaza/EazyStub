using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Http;
using System.Text;
using FluentAssertions;
using Latsos.Client;
using Latsos.Shared;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Latsos.Test
{
    [TestFixture]
    public class RequestRegistrationFixture
    {
        private readonly Fixture _fixture = new Fixture();

         
     

   
        [Test]
        public void GetHashCode_ShouldWork()
        {
            
            var dictionary = new ConcurrentDictionary<RequestRegistration, string>();

            var httpRequest1 = _fixture.Create<RequestRegistration>();
            var httpRequest2 = _fixture.Create<RequestRegistration>();

            dictionary.TryAdd(httpRequest1,"").Should().BeTrue();
            dictionary.TryAdd(httpRequest2,"").Should().BeTrue();


            var dictionary2 = new ConcurrentDictionary<RequestRegistration, string>();

            var httpRequest3 = _fixture.Create<RequestRegistration>();
            var httpRequest4 = httpRequest3.Copy();

            
            dictionary2.TryAdd(httpRequest3, "").Should().BeTrue();
            dictionary2.TryAdd(httpRequest4, "").Should().BeFalse();



        }
      
    }
}