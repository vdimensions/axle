using System.Diagnostics.CodeAnalysis;
using Axle.Verification;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Axle.Web.AspNetCore
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class ApplicationBuilderExtensions
    {
        private static IApplicationBuilder RegisterAspNetCoreModule(IApplicationBuilder builder, IWebHostBuilder webHostBuilder)
        {
            return builder
                .ConfigureDependencies(container => container.RegisterInstance(webHostBuilder, string.Empty))
                .ConfigureModules(m => m.Load<AspNetCoreModule>());
        }

        public static IApplicationBuilder UseAspNetCore(this IApplicationBuilder builder, IWebHostBuilder webHostBuilder)
        {
            builder.VerifyArgument(nameof(builder)).IsNotNull();
            webHostBuilder.VerifyArgument(nameof(webHostBuilder)).IsNotNull();
            return RegisterAspNetCoreModule(builder, webHostBuilder);
        }
        public static IApplicationBuilder UseAspNetCore(this IApplicationBuilder builder)
        {
            builder.VerifyArgument(nameof(builder)).IsNotNull();
            // TODO: expose command line args from the application
            return RegisterAspNetCoreModule(builder, WebHost.CreateDefaultBuilder());
        }
    }
}
