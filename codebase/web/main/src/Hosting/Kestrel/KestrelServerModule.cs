using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;
using Axle.Web.AspNetCore.Sdk;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Axle.Web.AspNetCore.Hosting.Kestrel
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [Module]
    [RequiresAspNetCore]
    internal sealed class KestrelServerModule 
        : AbstractConfigurableAspNetCoreModule<IKestrelServerConfigurer, WebHostBuilderContext, KestrelServerOptions>, 
          IWebHostConfigurer
    {
        void IWebHostConfigurer.Configure(IWebHostBuilder host) => host.UseKestrel(Configure);
    }
}