using System;
using System.Collections.Generic;
using Axle.Configuration;
using Axle.Modularity;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class UtilizesAspNetCoreAttribute : UtilizesAttribute
    {
        public UtilizesAspNetCoreAttribute() : base(typeof(AspNetCoreModule)) { }
    }

    [Module]
    internal sealed class AspNetCoreModule
    {
        private readonly IWebHostBuilder _host;
        private readonly Application _app;
        private readonly IConfiguration _appConfig;
        private readonly IList<IWebHostConfigurer> _whConfigurers = new List<IWebHostConfigurer>();
        private readonly IList<IServiceConfigurer> _serviceConfigurers = new List<IServiceConfigurer>();
        private readonly IList<IApplicationConfigurer> _appConfigurers = new List<IApplicationConfigurer>();
        private readonly AxleHttpContextAccessor _httpContextAccessor = new AxleHttpContextAccessor();

        public AspNetCoreModule(Application app, IConfiguration appConfig, IWebHostBuilder host)
        {
            _host = host;
            _app = app;
            _appConfig = appConfig;
        }

        [ModuleInit]
        internal void Init(ModuleExporter exporter)
        {
            exporter.Export<IHttpContextAccessor>(_httpContextAccessor);
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
            services.AddHttpContextAccessor();
        }

        private void ConfigureApp(Microsoft.AspNetCore.Builder.IApplicationBuilder app)
        {
            var services = app.ApplicationServices;
            services
               .GetRequiredService<IApplicationLifetime>()
               .ApplicationStopped.Register(_app.ShutDown);

            var accessor = services.GetRequiredService<IHttpContextAccessor>();
            _httpContextAccessor.Accessor = accessor;

            foreach (var cfg in _appConfigurers)
            {
                cfg.Configure(app);
            }
        }

        [ModuleEntryPoint]
        internal void Run(string[] args)
        {
            var host = _host;
            //
            // configure the web host with a re-adapted Axle application configuration
            //
            Microsoft.Extensions.Configuration.IConfigurationProvider axleConfigurationProvider = new Axle.Configuration.Adapters.AxleConfigurationProvider(_appConfig);
            host.UseConfiguration(new Microsoft.Extensions.Configuration.ConfigurationRoot(new[] { axleConfigurationProvider }));

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
