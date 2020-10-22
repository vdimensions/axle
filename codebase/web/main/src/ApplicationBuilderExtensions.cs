using System.Diagnostics.CodeAnalysis;
using Axle.Application;
using Axle.Verification;
using Axle.Web.AspNetCore.FileServer;
using Axle.Web.AspNetCore.Hosting;
using Axle.Web.AspNetCore.Routing;

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
        public static IApplicationBuilder UseAspNetCore(this IApplicationBuilder app, string[] args) 
            => UseAspNetCore(app, WebHost.CreateDefaultBuilder(args));
        
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public static IApplicationBuilder UseAspNetCore(this IApplicationBuilder app) 
            => UseAspNetCore(app, WebHost.CreateDefaultBuilder());
        
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public static IApplicationBuilder UseIISIntegration(this IApplicationBuilder app) 
            => app.ConfigureModules(m => m.Load<IISIntegrationModule>());
        
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public static IApplicationBuilder UseKestrel(this IApplicationBuilder app) 
            => app.ConfigureModules(m => m.Load<KestrelModule>());
        
        
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public static IApplicationBuilder UseStaticFiles(this IApplicationBuilder app) => 
            app.ConfigureModules(m => m.Load<StaticFilesModule>());


        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public static IApplicationBuilder UseRouting(this IApplicationBuilder app) =>
            app.ConfigureModules(m => m.Load<AspNetCoreRoutingModule>());
    }
}
