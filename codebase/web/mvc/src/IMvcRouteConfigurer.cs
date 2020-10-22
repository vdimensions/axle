using Microsoft.AspNetCore.Routing;

namespace Axle.Web.AspNetCore.Mvc
{
    [RequiresAspNetCoreMvc]
    public interface IMvcRouteConfigurer
    {
        void ConfigureRoutes(IRouteBuilder routeBuilder);
    }
}