using Axle.Configuration;
using Axle.Modularity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Axle.Web.AspNetCore.FileServer
{
    [Module]
    [Utilizes(typeof(DefaultFilesModule))]
    internal sealed class StaticFilesModule : IApplicationConfigurer
    {
        public StaticFilesModule(IConfiguration configuration)
        {
        }
        
        #if NETCOREAPP3_0_OR_NEWER
        void IApplicationConfigurer.Configure(IApplicationBuilder app, IWebHostEnvironment env)
        #else
        void IApplicationConfigurer.Configure(IApplicationBuilder app, IHostingEnvironment env)
        #endif
        {
            app.UseStaticFiles();
        }
    }
}