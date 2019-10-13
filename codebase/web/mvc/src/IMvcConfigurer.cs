using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Axle.Web.AspNetCore.Mvc
{
    [RequiresAspNetMvc]
    public interface IMvcConfigurer
    {
        void ConfigureMvc(IMvcBuilder builder);
    }
    [RequiresAspNetMvc]
    public interface IMvcRouteConfigurer
    {
        void ConfigureRoutes(IRouteBuilder routeBuilder);
    }
}