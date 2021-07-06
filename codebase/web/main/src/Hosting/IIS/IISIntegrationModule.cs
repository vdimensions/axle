using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;
using Axle.Web.AspNetCore.Hosting.Kestrel;
using Microsoft.AspNetCore.Hosting;

namespace Axle.Web.AspNetCore.Hosting.IIS
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [Module]
    [RequiresAspNetCore]
    [Utilizes(typeof(KestrelServerModule))]
    internal sealed class IISIntegrationModule : IWebHostConfigurer
    {
        void IWebHostConfigurer.Configure(IWebHostBuilder host) => host.UseIISIntegration();
    }
}