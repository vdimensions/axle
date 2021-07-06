using Axle.Web.AspNetCore.Sdk;
using Microsoft.AspNetCore.Mvc;

namespace Axle.Web.AspNetCore.Mvc
{
    [RequiresAspNetCoreMvc]
    public interface IMvcConfigurer : IAspNetCoreConfigurer<MvcOptions>
    {
    }
}