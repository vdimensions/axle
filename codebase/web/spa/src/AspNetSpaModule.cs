using Axle.Logging;
using Axle.Modularity;
using Axle.Web.AspNetCore.Mvc;
using Axle.Web.AspNetCore.Session;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;


namespace Axle.Web.AspNetCore.Spa
{
    [Module]
    [RequiresAspNetCore]
    [UtilizesAspNetSession] // If Session is used, SPA must be initialized after Session
    [UtilizesAspNetMvc]     // If MVC is used, SPA must be initialized after MVC
    public sealed class AspNetSpaModule : IServiceConfigurer, IApplicationConfigurer
    {
        private readonly ILogger _logger;

        public AspNetSpaModule(ILogger logger)
        {
            _logger = logger;
        }

        [ModuleInit]
        internal void Init()
        {
        }

        void IServiceConfigurer.Configure(IServiceCollection services)
        {
            //services.AddSpaPrerenderer();
        }

        void IApplicationConfigurer.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app, IHostingEnvironment _)
        {
            //app
        }

        [ModuleTerminate]
        internal void Terminate()
        {
        }
    }
}