using Axle.Configuration;
using Axle.Modularity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Axle.Web.AspNetCore.StaticFiles
{
    [Module]
    public sealed class DefaultFilesModule : IApplicationConfigurer
    {
        public DefaultFilesModule(IConfiguration configuration)
        {

        }

        void IApplicationConfigurer.Configure(
            Microsoft.AspNetCore.Builder.IApplicationBuilder app, 
            IHostingEnvironment env)
        {
            app.UseDefaultFiles();
        }
    }
}