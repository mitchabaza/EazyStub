﻿using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace EasyStub.Server
{
    /// <summary>
    /// Represents configuration for <see cref="IExceptionHandler"/> and <see cref="IExceptionLogger"/>.
    /// </summary>
    public class ServiceConfig
    {
        /// <summary>
        /// COnfigures custom implementations for: <see cref="IExceptionHandler"/> and <see cref="IExceptionLogger"/>.
        /// </summary>
        /// <param name="configuration"></param>
        public static void Configure(HttpConfiguration configuration)
        {
            //configuration.Services.Replace(typeof(IExceptionHandler), new ApiExceptionHandler());
            //configuration.Services.Register(typeof(IExceptionLogger), new ApiExceptionLogger());
        }
    }
}