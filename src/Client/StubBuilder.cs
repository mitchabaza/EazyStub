namespace EasyStub.Client
{
    public class StubBuilder
    {
        internal RequestBuilder RequestBuilder { get; }
        internal RequestBuilderFinisher RequestBuilderFinisher { get; set; }

        internal ResponseBuilder ResponseBuilder { get; }

        public StubBuilder()
        {
            RequestBuilder = new RequestBuilder(this);
            ResponseBuilder = new ResponseBuilder(this);
        }

        public StubBuilder(StubChannel channel)
        {
            Channel = channel;
            RequestBuilder = new RequestBuilder(this);
            ResponseBuilder = new ResponseBuilder(this);
        }

        public RequestBuilder AllRequests => RequestBuilder;

        public StubChannel Channel { get; }
    }
}