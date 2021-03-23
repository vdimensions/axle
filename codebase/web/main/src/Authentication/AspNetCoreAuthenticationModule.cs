using Axle.Modularity;
using Axle.Web.AspNetCore.Cors;
using Axle.Web.AspNetCore.Routing;
using Axle.Web.AspNetCore.Sdk;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
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