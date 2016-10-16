using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Latsos.Shared;

namespace Latsos.Client
{
    public class MockBuilder
    {
        internal RequestBuilder RequestBuilder { get; }
        internal ResponseBuilder ResponseBuilder { get; }

        public MockBuilder()
        {
            RequestBuilder= new RequestBuilder(this);
            ResponseBuilder = new ResponseBuilder(this);
        }

        public RequestBuilder Request()
        {
            return RequestBuilder;
        }
        
    }
}
