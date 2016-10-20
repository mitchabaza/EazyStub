using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Latsos.Shared;

namespace Latsos.Client
{
    public class StubBuilder
    {
        internal RequestBuilder RequestBuilder { get; }
        internal ResponseBuilder ResponseBuilder { get; }

        public StubBuilder()
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
