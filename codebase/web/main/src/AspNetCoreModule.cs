using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Axle.Configuration;
using Axle.DependencyInjection;
using Axle.Logging;
using Axle.Modularity;
using Axle.Web.AspNetCore.Lifecycle;
using Axle.Web.AspNetCore.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace Axle.Web.AspNetCore
{
    [Module]
    internal sealed class AspNetCoreModule : ILoggingServiceConfigurer
    {
        private readonly IWebHostBuilder _hostBuilder;
        private readonly IConfiguration _appConfig;
        private readonly IList<IWebHostConfigurer> _hostConfigurers = new List<IWebHostConfigurer>();
        private readonly IList<IServiceConfigurer> _serviceConfigurers = new List<IServiceConfigurer>();
        private readonly IList<IApplicationConfigurer> _appConfigurers = new List<IApplicationConfigurer>();
        private readonly IList<IAspNetCoreApplicationStartHandler> _appStartHandlers = new List<IAspNetCoreApplicationStartHandler>();
        private readonly IList<IAspNetCoreApplicationStoppedHandler> _appStoppedHandlers = new List<IAspNetCoreApplicationStoppedHandler>();
        private readonly IList<IAspNetCoreApplicationStoppingHandler> _appStoppingHandlers = new List<IAspNetCoreApplicationStoppingHandler>();
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private ILoggingServiceRegistry _loggingServiceRegistry;
        private Task _runTask;

        public AspNetCoreModule(IConfiguration appConfig, IWebHostBuilder hostBuilder)
        {
            _hostBuilder = hostBuilder;
            _appConfig = appConfig;
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleInit]
        internal void Init(IDependencyExporter exporter)
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

            if (_loggingServiceRegistry != null)
            {
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                var msLog = new MicrosoftLoggingService(loggerFactory);
                _loggingServiceRegistry.RegisterLoggingService(msLog);
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
                _cancellationTokenSource.Cancel();
            }
        }
        
        public void Configure(ILoggingServiceRegistry loggingServiceRegistry)
        {
            _loggingServiceRegistry = loggingServiceRegistry;
        }
        
        [ModuleReady]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        internal void Ready(string[] args)
        {
            _runTask = RunAsync();
        }

        private async Task RunAsync()
        {
            Microsoft.Extensions.Configuration.IConfigurationProvider axleConfigurationProvider = new Axle.Configuration.Adapters.AxleConfigurationProvider(_appConfig);
            string startupAssemblyKey = null, applicationKey = null, entryAssemblyName = Assembly.GetEntryAssembly().GetName().Name;
            var hostBuilder = _hostBuilder
                // configure the web host with a re-adapted Axle application configuration
                .UseConfiguration(new Microsoft.Extensions.Configuration.ConfigurationRoot(new[] { axleConfigurationProvider }))
                // ensure ApplicationKey setting is passed, and fallback to the executing assembly name
                .UseSetting(WebHostDefaults.ApplicationKey, applicationKey = (_hostBuilder.GetSetting(WebHostDefaults.ApplicationKey) ?? entryAssemblyName))
                // ensure StartupAssemblyKey setting is passed, and fallback to the executing assembly name
                .UseSetting(WebHostDefaults.StartupAssemblyKey, startupAssemblyKey = (_hostBuilder.GetSetting(WebHostDefaults.StartupAssemblyKey) ?? entryAssemblyName));
            
            await ConfigureHost(hostBuilder)
                .ConfigureServices(ConfigureServices)
                .Configure(ConfigureApp)
                // we need to restore the ApplicationKey/StartupAssemblyKey as they are being reset by the `Configure` method.
                // otherwise ASPNETCORE will not be able to auto-discover controllers
                .UseSetting(WebHostDefaults.ApplicationKey, applicationKey)
                .UseSetting(WebHostDefaults.StartupAssemblyKey, startupAssemblyKey)
                .Build()
                .RunAsync(_cancellationTokenSource.Token);
        }

        [ModuleEntryPoint]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        internal void Run(string[] args)
        {
            _runTask.Wait(_cancellationTokenSource.Token);
        }

        [ModuleTerminate]
        internal void Terminate()
        {
            _appStartHandlers.Clear();
            _appStoppingHandlers.Clear();
            _appStoppedHandlers.Clear();
            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel(false);
            }
            _cancellationTokenSource.Dispose();
            _runTask?.Dispose();
        }
    }
}
