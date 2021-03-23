using Axle.Web.AspNetCore.Sdk;
using Microsoft.Extensions.DependencyInjection;

namespace Axle.Web.AspNetCore.Mvc.RazorPages
{
    [RequiresAspNetCoreMvc]
    public interface IRazorPagesBuilderConfigurer : IAspNetCoreConfigurer<IMvcBuilder>
    {
    }
}