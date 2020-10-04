using Axle.Configuration;
using Axle.Modularity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Axle.Web.AspNetCore.FileServer
{
    [Module]
    internal sealed class DefaultFilesModule : IApplicationConfigurer
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