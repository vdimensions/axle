using Axle.Web.AspNetCore.Sdk;
using Microsoft.AspNetCore.Routing;

namespace Axle.Web.AspNetCore.Routing
{
    [RequiresAspNetCoreRouting]
    public interface IRoutingConfigurer : IAspNetCoreConfigurer<RouteOptions>
    {
    }
}