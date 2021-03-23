using Axle.Modularity;
using Axle.Web.AspNetCore.FileServer;
using Axle.Web.AspNetCore.Sdk;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Axle.Web.AspNetCore.Routing
{
    [Module]
    [RequiresAspNetCore]
    [UtilizesStaticFiles]
    internal sealed class AspNetCoreRoutingModule 
        : AbstractConfigurableAspNetCoreModule<IRoutingConfigurer, RouteOptions>, 
          IServiceConfigurer, 
          IApplicationConfigurer
    {
        void IServiceConfigurer.Configure(IServiceCollection services) => services.AddRouting(Configure);

        #if NETSTANDARD2_1_OR_NEWER
        void IApplicationConfigurer.Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
        }
        #else
        void IApplicationConfigurer.Configure(IApplicationBuilder app, IHostingEnvironment env) { }
        #endif
    }
}