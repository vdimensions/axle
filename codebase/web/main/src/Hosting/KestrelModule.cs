using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;
using Microsoft.AspNetCore.Hosting;

namespace Axle.Web.AspNetCore.Hosting
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [Module]
    [RequiresAspNetCore]
    internal sealed class KestrelModule : IWebHostConfigurer
    {
        void IWebHostConfigurer.Configure(IWebHostBuilder host) => host.UseKestrel();
    }
}