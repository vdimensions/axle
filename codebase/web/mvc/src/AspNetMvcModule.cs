using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Axle.Logging;
using Axle.Modularity;
using Axle.Web.AspNetCore.Session;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Axle.Web.AspNetCore.Mvc
{
    [Module]
    [RequiresAspNetCore]
    [UtilizesAspNetSession] // If Session is used, MVC must be initialized after Session
    public sealed class AspNetMvcModule : IServiceConfigurer, IApplicationConfigurer
    {
        private readonly ILogger _logger;
        private readonly IList<IMvcConfigurer> _configurers;
        private readonly IList<IMvcRouteConfigurer> _routeConfigurers;

        public AspNetMvcModule(ILogger logger)
        {
            _logger = logger;
            _configurers = new List<IMvcConfigurer>();
            _routeConfigurers = new List<IMvcRouteConfigurer>();
        }

        [ModuleInit]
        internal void Init()
        {
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyInitialized]
        internal void OnDependencyInitialized(IMvcConfigurer configurer) => _configurers.Add(configurer);
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyInitialized]
        internal void OnDependencyInitialized(IMvcRouteConfigurer configurer) => _routeConfigurers.Add(configurer);
        
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyTerminated]
        internal void OnDependencyTerminated(IMvcConfigurer configurer) => _configurers.Remove(configurer);
        
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyTerminated]
        internal void OnDependencyTerminated(IMvcRouteConfigurer configurer) => _routeConfigurers.Remove(configurer);

        void IServiceConfigurer.Configure(IServiceCollection services) => Configure(services.AddMvc());

        void IApplicationConfigurer.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app, IHostingEnvironment _)
        {
            if (_routeConfigurers.Count > 0)
            {
                app.UseMvc(Configure);
            }
            else
            {
                app.UseMvc();
            }
        }

        private void Configure(IMvcBuilder builder)
        {
            for (var i = 0; i < _configurers.Count; ++i)
            {
                _configurers[i].ConfigureMvc(builder);
            }
        }
        private void Configure(IRouteBuilder routeBuilder)
        {
            for (var i = 0; i < _configurers.Count; ++i)
            {
                _routeConfigurers[i].ConfigureRoutes(routeBuilder);
            }
        }

        [ModuleTerminate]
        internal void Terminate()
        {
        }
    }
}