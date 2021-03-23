using Axle.Web.AspNetCore.Sdk;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Axle.Web.AspNetCore.Mvc.RazorPages
{
    [RequiresAspNetCoreMvc]
    public interface IRazorPagesConfigurer : IAspNetCoreConfigurer<RazorPagesOptions>
    {
    }
}