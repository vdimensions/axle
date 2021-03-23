#if NETSTANDARD2_1_OR_NEWER
using Axle.Web.AspNetCore.Sdk;
using Microsoft.Extensions.DependencyInjection;

namespace Axle.Web.AspNetCore.Mvc
{
    [RequiresAspNetCoreMvc]
    public interface IControllersWithViewsConfigurer : IAspNetCoreConfigurer<IMvcBuilder>
    {
    }
}
#endif