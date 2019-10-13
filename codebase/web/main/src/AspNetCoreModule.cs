using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Axle.Configuration;
using Axle.Modularity;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;


namespace Axle.Web.AspNetCore
{
    [Module]
    internal sealed class AspNetCoreModule
    {
        private readonly IWebHostBuilder _host;
        private readonly Application _app;
        private readonly IConfiguration _appConfig;
        private readonly IList<IWebHostConfigurer> _whConfigurers = new List<IWebHostConfigurer>();
        private readonly IList<IServiceConfigurer> _serviceConfigurers = new List<IServiceConfigurer>();
        private readonly IList<IApplicationConfigurer> _appConfigurers = new List<IApplicationConfigurer>();

        public AspNetCoreModule(Application app, IConfiguration appConfig, IWebHostBuilder host)
        {
            _host = host;
            _app = app;
            _appConfig = appConfig;
        }

        [ModuleInit]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        internal void Init(ModuleExporter exporter)
        {
        }

        [ModuleDependencyInitialized]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        internal void OnWebHostConfigurerInitialized(IWebHostConfigurer cfg)
        {
            _whConfigurers.Add(cfg);
        }

        [ModuleDependencyInitialized]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        internal void OnServiceConfigurerInitialized(IServiceConfigurer cfg)
        {
            _serviceConfigurers.Add(cfg);
        }

        [ModuleDependencyInitialized]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        internal void OnApplicationConfigurerInitialized(IApplicationConfigurer cfg)
        {
            _appConfigurers.Add(cfg);
        }

        private class AxleStartupFilter : IStartupFilter
        {
            Action<Microsoft.AspNetCore.Builder.IApplicationBuilder> IStartupFilter.Configure(Action<Microsoft.AspNetCore.Builder.IApplicationBuilder> next)
            {
                return builder =>
                {
                    Delegate?.Invoke(builder);
                    next(builder);
                };
            }
            
            public Action<Microsoft.AspNetCore.Builder.IApplicationBuilder> Delegate;
        }
        
        private void ConfigureServices(IServiceCollection services)
        {
            for (var i = 0; i < _serviceConfigurers.Count; i++)
            {
                var cfg = _serviceConfigurers[i];
                cfg.Configure(services);
            }
            services.AddSingleton<IStartupFilter>(provider => new AxleStartupFilter {Delegate = ConfigureApp});
        }

        void ConfigureApp(Microsoft.AspNetCore.Builder.IApplicationBuilder app)
        {
            var services = app.ApplicationServices;
            services
                .GetRequiredService<IApplicationLifetime>()
                .ApplicationStopped.Register(_app.ShutDown);
            var hostingEnvironment = services.GetService<IHostingEnvironment>();
            for (var i = 0; i < _appConfigurers.Count; i++)
            {
                var cfg = _appConfigurers[i];
                cfg.Configure(app, hostingEnvironment);
            }
        }
        private void ConfigureHost(IWebHostBuilder host)
        {
            for (var i = 0; i < _whConfigurers.Count; i++)
            {
                var cfg = _whConfigurers[i];
                cfg.Configure(host);
            }
        }
        
        [ModuleEntryPoint]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        internal void Run(string[] args)
        {
            var host = _host;
            //
            // configure the web host with a re-adapted Axle application configuration
            //
            Microsoft.Extensions.Configuration.IConfigurationProvider axleConfigurationProvider = new Axle.Configuration.Adapters.AxleConfigurationProvider(_appConfig);
            host.UseConfiguration(new Microsoft.Extensions.Configuration.ConfigurationRoot(new[] { axleConfigurationProvider }));

            ConfigureHost(host);
            var applicationKey = host.GetSetting(WebHostDefaults.ApplicationKey);
            var startupAssemblyKey = host.GetSetting(WebHostDefaults.StartupAssemblyKey);
            host
                .ConfigureServices(ConfigureServices)
                //.Configure(ConfigureApp)
                .Configure(_ => { })
                // we need to restore the ApplicationKey/StartupAssemblyKey as they are being reset by the `Configure` method.
                .UseSetting(WebHostDefaults.ApplicationKey, applicationKey)
                .UseSetting(WebHostDefaults.StartupAssemblyKey, startupAssemblyKey)
                .Build()
                .Run();
        }
    }
}
