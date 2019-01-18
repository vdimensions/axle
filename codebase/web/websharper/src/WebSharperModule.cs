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
        public IServiceCollection Configure(IServiceCollection builder)
        {
            builder
                .AddAuthentication("WebSharper")
                .AddCookie("WebSharper", options => { });
            return builder;
        }
    }
}
