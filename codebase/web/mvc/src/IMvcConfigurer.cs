using Microsoft.Extensions.DependencyInjection;

namespace Axle.Web.AspNetCore.Mvc
{
    [RequiresAspNetMvc]
    public interface IMvcConfigurer
    {
        void ConfigureMvc(IMvcBuilder builder);
    }
}