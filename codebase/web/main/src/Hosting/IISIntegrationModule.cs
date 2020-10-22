using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;
using Microsoft.AspNetCore.Hosting;

namespace Axle.Web.AspNetCore.Hosting
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [Module]
    [RequiresAspNetCore]
    [Utilizes(typeof(KestrelModule))]
    internal sealed class IISIntegrationModule : IWebHostConfigurer
    {
        void IWebHostConfigurer.Configure(IWebHostBuilder host) => host.UseIISIntegration();
    }
}