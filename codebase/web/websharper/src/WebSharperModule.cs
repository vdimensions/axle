using Microsoft.Extensions.DependencyInjection;

using Axle.Modularity;
using Axle.Web.AspNetCore;
using Axle.Web.AspNetCore.Session;


namespace Axle.Web.WebSharper
{
    [Module]
    [RequiresAspNetCore]
    [UtilizesAspNetSession]
    internal sealed class WebSharperModule : IServiceConfigurer
    {
        public void Configure(IServiceCollection services)
        {
            services
                .AddAuthentication("WebSharper")
                .AddCookie("WebSharper", options => { });
        }
    }
}
