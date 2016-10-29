namespace EasyStub.Client
{
    public class RequestBuilder
    {
        internal StubBuilder StubBuilder { get; }

        public RequestBuilder(StubBuilder stubBuilder)
        {
            StubBuilder = stubBuilder;
        }


        public RequestBuilderFinisher WithPath(string path)
        {
            return new RequestBuilderFinisher(StubBuilder, path);
        }

      

    }
}