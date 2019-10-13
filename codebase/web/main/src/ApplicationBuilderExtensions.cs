using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Axle.Verification;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Axle.Web.AspNetCore
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class ApplicationBuilderExtensions
    {
        private static IApplicationBuilder RegisterAspNetCoreModule(IApplicationBuilder app, IWebHostBuilder webHostBuilder)
        {
            var name = Assembly.GetEntryAssembly().GetName().Name;
            webHostBuilder = webHostBuilder.UseSetting(WebHostDefaults.ApplicationKey, name);
            webHostBuilder = webHostBuilder.UseSetting(WebHostDefaults.StartupAssemblyKey, name);
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
        
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public static IApplicationBuilder UseKestrel(this IApplicationBuilder app) => app.ConfigureModules(m => m.Load<KestrelModule>());
    }
}
