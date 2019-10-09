using System.Diagnostics.CodeAnalysis;

using Axle.Modularity;

using Microsoft.AspNetCore.Hosting;


namespace Axle.Web.AspNetCore
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [Module]
    [RequiresAspNetCore]
    public sealed class KestrelModule : IWebHostConfigurer
    {
        void IWebHostConfigurer.Configure(IWebHostBuilder host) => host.UseKestrel().UseIISIntegration();
    }
}