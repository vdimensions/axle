using Microsoft.Extensions.DependencyInjection;

namespace Axle.Web.AspNetCore.Mvc
{
    [RequiresAspNetCoreMvc]
    public interface IMvcConfigurer
    {
        void ConfigureMvc(IMvcBuilder builder);
    }
}