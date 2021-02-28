using System;
using System.IO;
using System.Reflection;
using Axle.Configuration;
using Axle.DependencyInjection;
using Axle.Logging;
using Axle.Resources.Bundling;
using Axle.Resources.Extraction;

namespace Axle.Application
{
    /// <summary>
    /// An abstract class to serve as a base for implementing a custom application host.
    /// </summary>
    /// <seealso cref="IApplicationHost"/>
    public abstract class AbstractApplicationHost : IApplicationHost, IDisposable, ISetLoggingService
    {
        private readonly IConfiguration _hostConfiguration;
        private readonly IConfiguration _appConfiguration;

        /// <summary>
        /// Initializes a new instance of an implementation of <see cref="AbstractApplicationHost"/> with
        /// the specified parameters
        /// </summary>
        /// <param name="dependencyContainerFactory">
        /// The <see cref="IDependencyContainerFactory"/> that will be used to provide the application that will be
        /// created with this <see cref="AbstractApplicationHost"/> implementation with
        /// <see cref="IDependencyContainer"/> instances. 
        /// </param>
        /// <param name="loggingService">
        /// A <see cref="ILoggingService"/> to handle creation of <see cref="ILogger"/> objects for the application.
        /// </param>
        /// <param name="environmentName">
        /// A <see cref="string"/> representing the current environment's name. This is often used in conjunction with
        /// the application's <see cref="IConfiguration">configuration</see> as particular configuration entries
        /// may need to be overriden for different environments.
        /// </param>
        /// <param name="profiles">
        /// An optional list of activated profile names. Similar to the <paramref name="environmentName"/>, each profile
        /// could be associated with configuration entries that will get overriden with profile-specific values. Profile
        /// listing order determines overriding order, where the latest profile in the list determines which values to
        /// be resolved at runtime.
        /// </param>
        protected AbstractApplicationHost(
                IDependencyContainerFactory dependencyContainerFactory, 
                ILoggingService loggingService,
                string environmentName,
                params string[] profiles) 
            : this(dependencyContainerFactory, loggingService, null, null, environmentName, profiles) { }
        /// <summary>
        /// Initializes a new instance of an implementation of <see cref="AbstractApplicationHost"/> with
        /// the specified parameters
        /// </summary>
        /// <param name="dependencyContainerFactory">
        /// The <see cref="IDependencyContainerFactory"/> that will be used to provide the application that will be
        /// created with this <see cref="AbstractApplicationHost"/> implementation with
        /// <see cref="IDependencyContainer"/> instances. 
        /// </param>
        /// <param name="loggingService">
        /// A <see cref="ILoggingService"/> to handle creation of <see cref="ILogger"/> objects for the application.
        /// </param>
        /// <param name="applicationConfigName">
        /// A string representing the filename (without extension) of the application's configuration file.
        /// <para>
        /// Application configuration filename is formatted as <c>${applicationConfigName}.${extension}</c>.
        /// </para>
        /// <para>
        /// Environment-specific configuration filename is formatted as
        /// <c>${applicationConfigName}.${environmentName}.${extension}</c>.
        /// </para>
        /// <para>
        /// Profile-specific configuration filename is formatted as
        /// <c>${applicationConfigName}.${profile}.${extension}</c>.
        /// </para>
        /// </param>
        /// <param name="hostConfigName">
        /// A string representing the filename (without extension) of the application host configuration file. The host
        /// configuration serves to provide defaults to most application configuration settings, and may also vary based
        /// on the environment or the activate profiles.
        /// <para>
        /// Host configuration filename is formatted as <c>${hostConfigName}.${extension}</c>.
        /// </para>
        /// <para>
        /// Environment-specific host configuration filename is formatted as
        /// <c>${hostConfigName}.${environmentName}.${extension}</c>.
        /// </para>
        /// <para>
        /// Profile-specific host configuration filename is formatted as
        /// <c>${hostConfigName}.${profile}.${extension}</c>.
        /// </para>
        /// </param>
        /// <param name="environmentName">
        /// A <see cref="string"/> representing the current environment's name. This is often used in conjunction with
        /// the application's <see cref="IConfiguration">configuration</see> as particular configuration entries
        /// may need to be overriden for different environments.
        /// </param>
        /// <param name="profiles">
        /// An optional list of activated profile names. Similar to the <paramref name="environmentName"/>, each profile
        /// could be associated with configuration entries that will get overriden with profile-specific values. Profile
        /// listing order determines overriding order, where the latest profile in the list determines which values to
        /// be resolved at runtime.
        /// </param>
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
            var hostConfigResourceMgr = ApplicationHost.CreateResourceManager(this);
            SetupHostConfigurationResourceBundle(
                hostConfigResourceMgr.Bundles.Configure(Application.ConfigBundleName));
            var hostConfigStreamProvider = new ResourceConfigurationStreamProvider(hostConfigResourceMgr);
            
            hostConfig = LoadConfig(hostConfigFileName, hostConfigStreamProvider, environmentName, profiles);
            
            var appConfigResourceMgr = ApplicationHost.CreateResourceManager(this);
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

        public virtual IResourceExtractorRegistry ConfigureDefaultResourcePaths(IResourceExtractorRegistry extractors) 
            => extractors;

        /// <inheritdoc />
        public IDependencyContainerFactory DependencyContainerFactory { get; }

        /// <inheritdoc />
        public ILoggingService LoggingService { get; private set; }
        ILoggingService ISetLoggingService.LoggingService
        {
            get => LoggingService;
            set => LoggingService = value;
        }

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