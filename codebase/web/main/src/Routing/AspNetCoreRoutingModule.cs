using Axle.Modularity;
using Axle.Web.AspNetCore.FileServer;
#if NETSTANDARD2_1_OR_NEWER
using Microsoft.AspNetCore.Builder;
#endif
using Microsoft.Extensions.DependencyInjection;

namespace Axle.Web.AspNetCore.Routing
{
    [Module]
    [RequiresAspNetCore]
    [UtilizesStaticFiles]
    internal sealed class AspNetCoreRoutingModule : IServiceConfigurer, IApplicationConfigurer
    {
        void IServiceConfigurer.Configure(IServiceCollection services)
        {
            services.AddRouting();
        }

        #if NETSTANDARD2_1_OR_NEWER
        void IApplicationConfigurer.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            app.UseRouting();
        }
        #else
        void IApplicationConfigurer.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env) { }
        #endif
    }
}