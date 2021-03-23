using Microsoft.AspNetCore.Routing;

namespace Axle.Web.AspNetCore.Mvc.Routing
{
    [RequiresAspNetCoreMvc]
    public interface IRouteBuilderConfigurer
    {
        void ConfigureRoutes(IRouteBuilder routeBuilder);
    }
}