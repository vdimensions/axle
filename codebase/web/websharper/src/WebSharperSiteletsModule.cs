using Axle.Modularity;
using Axle.Web.AspNetCore;
using Axle.Web.AspNetCore.Session;

using WebSharper.AspNetCore;


namespace Axle.Web.WebSharper
{
    [Module]
    [RequiresAspNetCore]
    [UtilizesAspNetSession]
    [RequiresWebSharper]
    internal sealed class WebSharperSiteletsModule : IApplicationConfigurer
    {
        public Microsoft.AspNetCore.Builder.IApplicationBuilder Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder builder)
        {
            return builder.UseWebSharper();
        }
    }
}