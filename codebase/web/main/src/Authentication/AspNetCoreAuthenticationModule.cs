using Axle.Modularity;
using Axle.Web.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Axle.Web.AspNetCore.Authentication
{
    [Module]
    [RequiresAspNetCore]
    [UtilizesAspNetCoreRouting]
    internal sealed class AspNetCoreAuthenticationModule : IServiceConfigurer, IApplicationConfigurer
    {
        void IServiceConfigurer.Configure(IServiceCollection services)
        {
            services.AddAuthentication();
        }

        #if NETSTANDARD2_1_OR_NEWER
        void IApplicationConfigurer.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        #else
        void IApplicationConfigurer.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        #endif
        {
            app.UseAuthentication();
        }
    }
}