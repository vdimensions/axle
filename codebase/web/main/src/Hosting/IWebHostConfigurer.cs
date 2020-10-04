using Microsoft.AspNetCore.Hosting;

namespace Axle.Web.AspNetCore.Hosting
{
    [RequiresAspNetCore]
    public interface IWebHostConfigurer
    {
        void Configure(IWebHostBuilder host);
    }
}