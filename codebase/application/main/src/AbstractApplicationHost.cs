using System;
using System.IO;
using System.Reflection;
using Axle.Configuration;
using Axle.DependencyInjection;
using Axle.Logging;
using Axle.Resources;
using Axle.Resources.Bundling;

namespace Axle
{
    /// <summary>
    /// An abstract class to serve as a base for implementing a custom application host.
    /// </summary>
    /// <seealso cref="IApplicationHost"/>
    public abstract class AbstractApplicationHost : IApplicationHost, IDisposable
    {
        private readonly IConfiguration _hostConfiguration;
        private readonly IConfiguration _appConfiguration;

        protected AbstractApplicationHost(
                IDependencyContainerFactory dependencyContainerFactory, 
                ILoggingService loggingService,
                string environmentName,
                params string[] profiles) 
            : this(dependencyContainerFactory, loggingService, null, null, environmentName, profiles) { }
        protected AbstractApplicationHost(
            IDependencyContainerFactory dependencyContainerFactory, 
            ILoggingService loggingService, 
            string applicationConfigName,
            string hostConfigName,
            string environmentName,
            params string[] profiles)
        {
            DependencyContainerFactory = dependencyContainerFactory ?? new AxleDependencyContainerFactory();
            LoggingService = loggingService;
            EnvironmentName = environmentName;
            LoadConfiguration(
                HostConfigFileName = hostConfigName ?? "host", 
                AppConfigFileName = applicationConfigName ?? "application", 
                environmentName, 
                profiles, 
                out _hostConfiguration, 
                out _appConfiguration);
        }
        
        private void LoadConfiguration(
            string hostConfigFileName, 
            string appConfigFileName, 
            string environmentName, 
            string[] profiles, 
            out IConfiguration hostConfig, 
            out IConfiguration appConfig)
        {
            var hostConfigResourceMgr = new DefaultResourceManager();
            SetupHostConfigurationResourceBundle(
                hostConfigResourceMgr.Bundles.Configure(Application.ConfigBundleName));
            var hostConfigStreamProvider = new ResourceConfigurationStreamProvider(hostConfigResourceMgr);
            
            hostConfig = LoadConfig(hostConfigFileName, hostConfigStreamProvider, environmentName, profiles);
            
            var appConfigResourceMgr = new DefaultResourceManager();
            SetupAppConfigurationResourceBundle(
                appConfigResourceMgr.Bundles.Configure(Application.ConfigBundleName));
            var appConfigStreamProvider = new ResourceConfigurationStreamProvider(appConfigResourceMgr);
            
            appConfig = LoadConfig(appConfigFileName, appConfigStreamProvider, environmentName, profiles);
        }

        private static IConfiguration LoadConfig(
            string configFileName, 
            ResourceConfigurationStreamProvider configStreamProvider, 
            string environmentName, 
            string[] profiles)
        {
            var tmpConfig = Application.Configure(
                new LayeredConfigManager(), 
                configFileName, 
                configStreamProvider,
                string.Empty);
            foreach (var profile in profiles)
            {
                tmpConfig = Application.Configure(tmpConfig, configFileName, configStreamProvider, profile);
            }

            tmpConfig = Application.Configure(tmpConfig, configFileName, configStreamProvider, environmentName);
            return tmpConfig.LoadConfiguration();
        }

        /// <summary>
        /// Allows configuring resource providers for loading the application-host specific configuration.
        /// </summary>
        /// <param name="bundle">
        /// A reference to the <see cref="IConfigurableBundleContent"/> that is used to obtain the application-host
        /// configuration.
        /// </param>
        protected virtual void SetupHostConfigurationResourceBundle(IConfigurableBundleContent bundle)
        {
            var assembly = GetType()
                #if NETSTANDARD || NET45_OR_NEWER
                .GetTypeInfo()
                #endif
                .Assembly;
            var asmName = assembly.GetName();
            bundle.Register(new Uri($"assembly://{asmName.Name}", UriKind.Absolute));
        }
        
        /// <summary>
        /// Allows configuring resource providers for loading the application-host specific configuration.
        /// </summary>
        /// <param name="bundle">
        /// A reference to the <see cref="IConfigurableBundleContent"/> that is used to obtain the application-host
        /// configuration.
        /// </param>
        protected virtual void SetupAppConfigurationResourceBundle(IConfigurableBundleContent bundle)
        {
            #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
            var assembly = Assembly.GetEntryAssembly();
            if (assembly == null)
            {
                return;
            }
            var asmName = assembly.GetName();
            var asmLoc = Path.GetDirectoryName(assembly.Location);
            if (asmLoc != null)
            {
                bundle.Register(new Uri(asmLoc, UriKind.Absolute));
            }
            bundle.Register(new Uri($"assembly://{asmName.Name}", UriKind.Absolute));
            #else
            return;
            #endif
        }
        
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">
        /// A boolean flag indicating whether the current method is invoked as part of a
        /// <see cref="IDisposable.Dispose"/> call. In that case, its value is passed in as <c>true</c> and any
        /// resources allocated by the current instance will be released.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (LoggingService is IDisposable disposableLoggingServiceProvider)
                {
                    disposableLoggingServiceProvider.Dispose();
                }
                if (DependencyContainerFactory is IDisposable disposableDependencyContainerProvider)
                {
                    disposableDependencyContainerProvider.Dispose();
                }
            }
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() => Dispose(true);
        void IDisposable.Dispose() => Dispose(true);

        /// <inheritdoc />
        public IDependencyContainerFactory DependencyContainerFactory { get; }

        /// <inheritdoc />
        public ILoggingService LoggingService { get; }

        /// <inheritdoc />
        public string EnvironmentName { get; }

        /// <inheritdoc />
        public string AppConfigFileName { get; }

        /// <inheritdoc />
        public string HostConfigFileName { get; }

        /// <inheritdoc />
        public virtual IConfiguration HostConfiguration => _hostConfiguration;

        /// <inheritdoc />
        public virtual IConfiguration AppConfiguration => _appConfiguration;
    }
}