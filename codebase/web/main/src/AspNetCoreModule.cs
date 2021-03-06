﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Axle.Configuration;
using Axle.Modularity;
using Axle.Web.AspNetCore.Lifecycle;
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
        private readonly IList<IWebHostConfigurer> _hostConfigurers = new List<IWebHostConfigurer>();
        private readonly IList<IServiceConfigurer> _serviceConfigurers = new List<IServiceConfigurer>();
        private readonly IList<IApplicationConfigurer> _appConfigurers = new List<IApplicationConfigurer>();
        private readonly IList<IAspNetCoreApplicationStartHandler> _appStartHandlers = new List<IAspNetCoreApplicationStartHandler>();
        private readonly IList<IAspNetCoreApplicationStoppedHandler> _appStoppedHandlers = new List<IAspNetCoreApplicationStoppedHandler>();
        private readonly IList<IAspNetCoreApplicationStoppingHandler> _appStoppingHandlers = new List<IAspNetCoreApplicationStoppingHandler>();

        public AspNetCoreModule(Application app, IConfiguration appConfig, IWebHostBuilder host)
        {
            _host = host;
            _app = app;
            _appConfig = appConfig;
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleInit]
        internal void Init(ModuleExporter exporter)
        {
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyInitialized]
        internal void OnDependencyInitialized(IWebHostConfigurer cfg) => _hostConfigurers.Add(cfg);

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyInitialized]
        internal void OnDependencyInitialized(IServiceConfigurer cfg) => _serviceConfigurers.Add(cfg);

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyInitialized]
        internal void OnDependencyInitialized(IApplicationConfigurer cfg) => _appConfigurers.Add(cfg);

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyInitialized]
        internal void OnDependencyInitialized(IAspNetCoreApplicationStartHandler handler) => _appStartHandlers.Add(handler);

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyInitialized]
        internal void OnDependencyInitialized(IAspNetCoreApplicationStoppingHandler handler) => _appStoppingHandlers.Add(handler);

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyInitialized]
        internal void OnDependencyInitialized(IAspNetCoreApplicationStoppedHandler handler) => _appStoppedHandlers.Add(handler);
        
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyTerminated]
        internal void OnDependencyTerminated(IAspNetCoreApplicationStartHandler handler) => _appStartHandlers.Remove(handler);

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyTerminated]
        internal void OnDependencyTerminated(IAspNetCoreApplicationStoppingHandler handler) => _appStoppingHandlers.Remove(handler);

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyTerminated]
        internal void OnDependencyTerminated(IAspNetCoreApplicationStoppedHandler handler) => _appStoppedHandlers.Remove(handler);

        private IWebHostBuilder ConfigureHost(IWebHostBuilder host)
        {
            for (var i = 0; i < _hostConfigurers.Count; i++)
            {
                var cfg = _hostConfigurers[i];
                cfg.Configure(host);
            }
            return host;
        }
        
        private void ConfigureServices(IServiceCollection services)
        {
            for (var i = 0; i < _serviceConfigurers.Count; i++)
            {
                var cfg = _serviceConfigurers[i];
                cfg.Configure(services);
            }
        }

        void ConfigureApp(Microsoft.AspNetCore.Builder.IApplicationBuilder app)
        {
            var services = app.ApplicationServices;
            var lifeTime = services.GetRequiredService<IApplicationLifetime>();
            lifeTime.ApplicationStarted.Register(OnApplicationStarted);
            lifeTime.ApplicationStopping.Register(OnApplicationStopping);
            lifeTime.ApplicationStopped.Register(OnApplicationStopped);
            
            var hostingEnvironment = services.GetService<IHostingEnvironment>();
            for (var i = 0; i < _appConfigurers.Count; i++)
            {
                var cfg = _appConfigurers[i];
                cfg.Configure(app, hostingEnvironment);
            }
        }

        private void OnApplicationStarted()
        {
            var handlers = _appStartHandlers.ToArray();
            for (var i = 0; i < handlers.Length; ++i)
            {
                handlers[i].OnApplicationStart();
            }
        }
        private void OnApplicationStopping()
        {
            var handlers = _appStoppingHandlers.ToArray();
            for (var i = handlers.Length - 1; i >= 0; --i)
            {
                handlers[i].OnApplicationStopping();
            }
        }
        private void OnApplicationStopped()
        {
            try
            {
                var handlers = _appStoppedHandlers.ToArray();
                for (var i = handlers.Length - 1; i >= 0; --i)
                {
                    handlers[i].OnApplicationStopped();
                }
            }
            finally
            {
                _app.ShutDown();
            }
        }

        [ModuleEntryPoint]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        internal void Run(string[] args)
        {
            Microsoft.Extensions.Configuration.IConfigurationProvider axleConfigurationProvider = new Axle.Configuration.Adapters.AxleConfigurationProvider(_appConfig);
            string startupAssemblyKey = null, applicationKey = null, entryAssemblyName = Assembly.GetEntryAssembly().GetName().Name;
            var host = _host
                // configure the web host with a re-adapted Axle application configuration
                .UseConfiguration(new Microsoft.Extensions.Configuration.ConfigurationRoot(new[] { axleConfigurationProvider }))
                // ensure ApplicationKey setting is passed, and fallback to the executing assembly name
                .UseSetting(WebHostDefaults.ApplicationKey, applicationKey = (_host.GetSetting(WebHostDefaults.ApplicationKey) ?? entryAssemblyName))
                // ensure StartupAssemblyKey setting is passed, and fallback to the executing assembly name
                .UseSetting(WebHostDefaults.StartupAssemblyKey, startupAssemblyKey = (_host.GetSetting(WebHostDefaults.StartupAssemblyKey) ?? entryAssemblyName));
            
            ConfigureHost(host)
                .ConfigureServices(ConfigureServices)
                .Configure(ConfigureApp)
                // we need to restore the ApplicationKey/StartupAssemblyKey as they are being reset by the `Configure` method.
                // otherwise ASPNETCORE will not be able to auto-discover controllers
                .UseSetting(WebHostDefaults.ApplicationKey, applicationKey)
                .UseSetting(WebHostDefaults.StartupAssemblyKey, startupAssemblyKey)
                .Build()
                .Run();
        }
        
        [ModuleTerminate]
        internal void Terminate()
        {
            _appStartHandlers.Clear();
            _appStoppingHandlers.Clear();
            _appStoppedHandlers.Clear();
        }
    }
}
