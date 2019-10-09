using Microsoft.Extensions.DependencyInjection;

namespace Axle.Web.AspNetCore
{
    [RequiresAspNetCore]
    public interface IServiceConfigurer
    {
        void Configure(IServiceCollection services);
    }
}