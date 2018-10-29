using System;
using System.Diagnostics.CodeAnalysis;

using Axle.Modularity;

using Microsoft.AspNetCore.Hosting;


namespace Axle.Web.AspNetCore
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresKestrelAttribute : RequiresAttribute
    {
        public RequiresKestrelAttribute() : base(typeof(KestrelModule)) { }
    }

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [Module]
    [RequiresAspNetCore]
    public sealed class KestrelModule : IWebHostConfigurer
    {
        IWebHostBuilder IWebHostConfigurer.Configure(IWebHostBuilder host)
        {
            return host.UseKestrel().UseIISIntegration();
        }
    }
}