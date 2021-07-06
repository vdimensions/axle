using Axle.Web.AspNetCore.Sdk;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Axle.Web.AspNetCore.Hosting.Kestrel
{
    [RequiresKestrel]
    public interface IKestrelServerConfigurer : IAspNetCoreConfigurer<WebHostBuilderContext, KestrelServerOptions>
    {
    }
}
