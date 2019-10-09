using Microsoft.AspNetCore.Hosting;

namespace Axle.Web.AspNetCore
{
    [RequiresAspNetCore]
    public interface IApplicationConfigurer
    {
        void Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app, IHostingEnvironment env);
    }
}