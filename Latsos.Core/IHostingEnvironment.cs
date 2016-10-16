using System.Web.Hosting;

namespace Latsos.Core
{
    public interface IHostingEnvironment
    {
         string ApplicationVirtualPath { get; }
    }

    public class DefaultHostingEnvironment : IHostingEnvironment
    {
        public string ApplicationVirtualPath => HostingEnvironment.ApplicationVirtualPath;
    }
}