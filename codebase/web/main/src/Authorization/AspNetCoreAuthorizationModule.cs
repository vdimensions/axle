using Axle.Modularity;
using Axle.Web.AspNetCore.Authentication;
#if NETSTANDARD2_1_OR_NEWER
using Microsoft.AspNetCore.Builder;
#endif
using Microsoft.Extensions.DependencyInjection;

namespace Axle.Web.AspNetCore.Authorization
{
    [Module]
    [RequiresAspNetCore]
    [UtilizesAspNetCoreAuthentication]
    internal sealed class AspNetCoreAuthorizationModule : IServiceConfigurer, IApplicationConfigurer
    {
        void IServiceConfigurer.Configure(IServiceCollection services)
        {
            //services.AddAuthorization();
        }

        #if NETSTANDARD2_1_OR_NEWER
        void IApplicationConfigurer.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env) 
        {
            app.UseAuthorization();
        }
        #else
        void IApplicationConfigurer.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env) { }
        #endif
    }
}