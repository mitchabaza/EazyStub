using System; 
using FluentAssertions;
using Latsos.Client;
using Latsos.Shared;
using NUnit.Framework;

namespace Latsos.Test.Client
{
    [TestFixture]
    public class RequestBuilderFixture
    {
        [Test]
        public void RequestBuilder_ShouldCreateRequest_WhenQueryStringSpecified()
        {
            var builder = new StubBuilder();

            var id = Guid.NewGuid().ToString();
            var registration = builder.Request()
                .QueryString("id", id)
                .Path("buzz/dance")
                .Build();

            registration.ShouldBeEquivalentTo(
                new RequestRegistration()
                {
                    Headers = MatchRule<Headers>.Default,
                    Port = MatchRule<int>.Default,
                    LocalPath  = "/buzz/dance",
                    Method= MatchRule<Method>.Default,
                    Query = new MatchRule<string>( $"?id={id}")

                });
        }

        [Test]
        public void RequestBuilder_ShouldCreateRequest_WhenQueryStringAndMethodSpecified()
        {
            var builder = new StubBuilder();

            var id = Guid.NewGuid().ToString();
            var registration = builder.Request()
                .QueryString("id", id).QueryString("jack","jill")
                .Path("etc/1").Method(Method.Post)
                .Build();

            registration.ShouldBeEquivalentTo(
                new RequestRegistration()
                {
                    Headers = MatchRule<Headers>.Default,
                    Port = MatchRule<int>.Default,
                    LocalPath = "/etc/1",
                    Method = new MatchRule<Method>( Method.Post),
                    Query = new MatchRule<string>( $"?id={id}&jack=jill")

                });
        }
        [Test]
        public void RequestBuilder_ShouldCreateRequest_WhenQueryStringAndMethodAndPortSpecified()
        {
            var builder = new StubBuilder();

            var id = Guid.NewGuid().ToString();
            var registration = builder.Request()
                .QueryString("tranId", id).QueryString("orgId", "64556456456").QueryString("custid", "654654")
                .Path("customer/delete/645564").Method(Method.Post)
                .Port(9999)
                .Build();

            registration.ShouldBeEquivalentTo(
                new RequestRegistration()
                {
                    Headers = MatchRule<Headers>.Default,
                    Port = new MatchRule<int>(9999),
                    LocalPath = "/customer/delete/645564",
                    Method = new MatchRule<Method>( Method.Post),
                    Query = new MatchRule<string>( $"?tranId={id}&orgId=64556456456&custid=654654")

                });
        }


        [Test]
        public void RequestBuilder_ShouldThrow_WhenNoPathSpecified()
        {
            var builder = new StubBuilder();

            Action action=()=>builder.Request().Build();
            action.ShouldThrowExactly<ArgumentNullException>();
        }
    }
}