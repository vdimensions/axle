using Axle.Web.AspNetCore.Routing;
using Axle.Web.AspNetCore.Sdk;
using Microsoft.AspNetCore.Routing;

namespace Axle.Web.AspNetCore.Mvc.Routing
{
    [RequiresAspNetCoreRouting]
    [RequiresAspNetCoreMvc]
    public interface IRouteBuilderConfigurer : IAspNetCoreConfigurer<IRouteBuilder>
    {
    }
}