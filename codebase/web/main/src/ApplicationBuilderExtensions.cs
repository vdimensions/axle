using System.Diagnostics.CodeAnalysis;
using Axle.Verification;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Axle.Web.AspNetCore
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class ApplicationBuilderExtensions
    {
        private static IApplicationBuilder RegisterAspNetCoreModule(IApplicationBuilder app, IWebHostBuilder webHostBuilder)
        {
            return app
                .ConfigureDependencies(container => container.RegisterInstance(webHostBuilder, string.Empty))
                .ConfigureModules(m => m.Load<AspNetCoreModule>());
        }

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public static IApplicationBuilder UseAspNetCore(this IApplicationBuilder app, IWebHostBuilder webHostBuilder)
        {
            app.VerifyArgument(nameof(app)).IsNotNull();
            webHostBuilder.VerifyArgument(nameof(webHostBuilder)).IsNotNull();
            return RegisterAspNetCoreModule(app, webHostBuilder);
        }
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public static IApplicationBuilder UseAspNetCore(this IApplicationBuilder app, string[] args) => UseAspNetCore(app, WebHost.CreateDefaultBuilder(args));
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public static IApplicationBuilder UseAspNetCore(this IApplicationBuilder app) => UseAspNetCore(app, WebHost.CreateDefaultBuilder());
    }
}
