using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using EasyStub.Core;
using MB.Owin.Logging.Log4Net;
using Microsoft.Owin.Logging;
using Owin;

namespace EasyStub.Server
{
    /// <summary>
    /// Represent Autofac configuration.
    /// </summary>
    public static class AutofacConfig
    {
        /// <summary>
        /// Configured instance of <see cref="IContainer"/>
        /// <remarks><see cref="AutofacConfig.Configure"/> must be called before trying to get Container instance.</remarks>
        /// </summary>
        public static IContainer Container;

        /// <summary>
        /// Initializes and configures instance of <see cref="IContainer"/>.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        public static void Configure(IAppBuilder app, HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            app.UseLog4Net("~/log4net.config");

            var logger = app.CreateLogger<ILogger>();
            builder.RegisterInstance(logger).As<ILogger>();

            // Other components can be registered here.
            builder.RegisterType<StubRequestHandler>().As<DelegatingHandler>().InstancePerRequest();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterAssemblyTypes(typeof(RequestEvaluator).Assembly).AsImplementedInterfaces().SingleInstance();
       
            Container = builder.Build();

            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
        }
    }
}