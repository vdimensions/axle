using Microsoft.Extensions.DependencyInjection;
using WebSharper.AspNetCore;
using Axle.Modularity;
using Axle.Web.AspNetCore;
using Axle.Web.AspNetCore.Session;


namespace Axle.Web.WebSharper
{
    [Module]
    [RequiresAspNetCore]
    [UtilizesAspNetSession]
    internal sealed class WebSharperModule : IServiceConfigurer, IApplicationConfigurer
    {
        public IServiceCollection Configure(IServiceCollection builder)
        {
            builder
                .AddAuthentication("WebSharper")
                .AddCookie("WebSharper", options => { });
            return builder;
        }

        public Microsoft.AspNetCore.Builder.IApplicationBuilder Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder builder)
        {
            return builder.UseWebSharper();
        }
    }
}
