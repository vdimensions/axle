using Axle.Modularity;
using Axle.Web.AspNetCore.Authentication;
using Axle.Web.AspNetCore.Cors;
using Axle.Web.AspNetCore.Routing;
using Axle.Web.AspNetCore.Sdk;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Axle.Web.AspNetCore.Authorization
{
    [Module]
    [RequiresAspNetCore]
    [UtilizesAspNetCoreRouting]
    [UtilizesAspNetCoreAuthentication]
    [UtilizesAspNetCoreCors]
    internal sealed class AspNetCoreAuthorizationModule 
        : AbstractConfigurableAspNetCoreModule<IAuthorizationConfigurer, AuthorizationOptions>,
          IServiceConfigurer, 
          IApplicationConfigurer
    {
        void IServiceConfigurer.Configure(IServiceCollection services)
        {
            services.AddAuthorization(Configure);
        }

        #if NETCOREAPP3_0_OR_NEWER
        void IApplicationConfigurer.Configure(IApplicationBuilder app, IWebHostEnvironment env) 
        {
            app.UseAuthorization();
        }
        #else
        void IApplicationConfigurer.Configure(IApplicationBuilder app, IHostingEnvironment env) { }
        #endif
    }
}