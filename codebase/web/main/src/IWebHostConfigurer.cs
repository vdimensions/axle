using Microsoft.AspNetCore.Hosting;

namespace Axle.Web.AspNetCore
{
    [RequiresAspNetCore]
    public interface IWebHostConfigurer
    {
        void Configure(IWebHostBuilder host);
    }
}