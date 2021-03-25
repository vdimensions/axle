using Axle.Web.AspNetCore.Sdk;
using Microsoft.Extensions.DependencyInjection;

namespace Axle.Web.AspNetCore.Mvc
{
    [RequiresAspNetCoreMvc]
    public interface IControllersConfigurer : IAspNetCoreConfigurer<IMvcBuilder>
    {
    }
}
