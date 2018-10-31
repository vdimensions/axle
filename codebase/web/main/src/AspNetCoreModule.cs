using System;
using System.Collections.Generic;

using Axle.Modularity;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;


namespace Axle.Web.AspNetCore
{
    [RequiresAspNetCore]
    public interface IWebHostConfigurer
    {
        IWebHostBuilder Configure(IWebHostBuilder builder);
    }
    [RequiresAspNetCore]
    public interface IServiceConfigurer
    {
        IServiceCollection Configure(IServiceCollection builder);
    }
    [RequiresAspNetCore]
    public interface IApplicationConfigurer
    {
        Microsoft.AspNetCore.Builder.IApplicationBuilder Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder builder);
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresAspNetCoreAttribute : RequiresAttribute
    {
        public RequiresAspNetCoreAttribute() : base(typeof(AspNetCoreModule)) { }
    }

    [Module]
    internal sealed class AspNetCoreModule
    {
        private readonly IWebHostBuilder _host;
        private readonly Application _app;
        private readonly IList<IWebHostConfigurer> _whConfigurers = new List<IWebHostConfigurer>();
        private readonly IList<IServiceConfigurer> _serviceConfigurers = new List<IServiceConfigurer>();
        private readonly IList<IApplicationConfigurer> _appConfigurers = new List<IApplicationConfigurer>();

        public AspNetCoreModule(Application app, IWebHostBuilder host)
        {
            _host = host;
            _app = app;
        }

        [ModuleDependencyInitialized]
        internal void OnWebHostConfigurerInitialized(IWebHostConfigurer cfg)
        {
            _whConfigurers.Add(cfg);
        }

        [ModuleDependencyInitialized]
        internal void OnServiceConfigurerInitialized(IServiceConfigurer cfg)
        {
            _serviceConfigurers.Add(cfg);
        }

        [ModuleDependencyInitialized]
        internal void OnApplicationConfigurerInitialized(IApplicationConfigurer cfg)
        {
            _appConfigurers.Add(cfg);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            foreach (var cfg in _serviceConfigurers)
            {
                cfg.Configure(services);
            }
        }

        private void ConfigureApp(Microsoft.AspNetCore.Builder.IApplicationBuilder app)
        {
            app.ApplicationServices
               .GetRequiredService<IApplicationLifetime>()
               .ApplicationStopped.Register(_app.ShutDown);

            foreach (var cfg in _appConfigurers)
            {
                cfg.Configure(app);
            }
        }

        [ModuleEntryPoint]
        internal void Run(string[] args)
        {
            var host = _host;

            foreach (var cfg in _whConfigurers)
            {
                cfg.Configure(host);
            }

            host
                .ConfigureServices(ConfigureServices)
                .Configure(ConfigureApp)
                .Build()
                .Run();
        }
    }
}
