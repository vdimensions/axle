using Microsoft.AspNetCore.Routing;

namespace Axle.Web.AspNetCore.Mvc
{
    [RequiresAspNetMvc]
    public interface IMvcRouteConfigurer
    {
        void ConfigureRoutes(IRouteBuilder routeBuilder);
    }
}