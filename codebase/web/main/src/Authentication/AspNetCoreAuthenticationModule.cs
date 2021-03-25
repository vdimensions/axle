using Axle.Modularity;
using Axle.Web.AspNetCore.Cors;
using Axle.Web.AspNetCore.Routing;
using Axle.Web.AspNetCore.Sdk;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Axle.Web.AspNetCore.Authentication
{
    [Module]
    [RequiresAspNetCore]
    [UtilizesAspNetCoreRouting]
    [UtilizesAspNetCoreCors]
    internal sealed class AspNetCoreAuthenticationModule 
        : AbstractConfigurableAspNetCoreModule<IAuthenticationConfigurer, AuthenticationOptions>,
          IServiceConfigurer, 
          IApplicationConfigurer
    {
        void IServiceConfigurer.Configure(IServiceCollection services)
        {
            services.AddAuthentication(Configure);
        }

        #if NETCOREAPP3_0_OR_NEWER
        void IApplicationConfigurer.Configure(IApplicationBuilder app, IWebHostEnvironment env)
        #else
        void IApplicationConfigurer.Configure(IApplicationBuilder app, IHostingEnvironment env)
        #endif
        {
            app.UseAuthentication();
        }
    }
}