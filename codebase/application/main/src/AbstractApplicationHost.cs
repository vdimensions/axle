using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Axle.Configuration;
using Axle.DependencyInjection;
using Axle.Logging;
#if NET35_OR_NEWER && !NET45_OR_NEWER && !NETSTANDARD
using Axle.Reflection.Extensions.Assembly;
#endif
using Axle.Resources;
using Axle.Resources.Bundling;
using Axle.Text;

namespace Axle
{
    /// <summary>
    /// An abstract class to serve as a base for implementing a custom application host.
    /// </summary>
    /// <seealso cref="IApplicationHost"/>
    public abstract class AbstractApplicationHost : IApplicationHost, IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly string[] _logo;

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
            LoggingService = new AggregatingLoggingService();
            if (loggingService != null)
            {
                ((ILoggingServiceRegistry) LoggingService).RegisterLoggingService(loggingService);
            }
            EnvironmentName = environmentName;
            ApplicationConfigFileName = applicationConfigName ?? "application";
            LoadConfiguration(HostConfigFileName = hostConfigName ?? "host", out _configuration, out _logo, environmentName, profiles);
        }
        
        private void LoadConfiguration(
            string configFileName, 
            out IConfiguration config, 
            out string[] logo, 
            string environmentName, 
            params string[] profiles)
        {
            
            var resourceManager = new DefaultResourceManager();
            SetupConfigurationResourceBundle(
                resourceManager.Bundles
                    .Configure(Application.ConfigBundleName));
            var configStreamProvider = new ResourceConfigurationStreamProvider(resourceManager);
            
            var tmpConfig = Application.Configure(new LayeredConfigManager(), configFileName, configStreamProvider, string.Empty);
            foreach (var profile in profiles)
            {
                tmpConfig = Application.Configure(tmpConfig, configFileName, configStreamProvider, profile);
            }
            tmpConfig = Application.Configure(tmpConfig, configFileName, configStreamProvider, environmentName);
            config = tmpConfig.LoadConfiguration();
            
            var logoStr = resourceManager.Load<string>(Application.ConfigBundleName, "logo.txt", CultureInfo.InvariantCulture);
            if (logoStr != null)
            {
                var assembly = GetType()
                    #if NETSTANDARD || NET45_OR_NEWER
                    .GetTypeInfo()
                    #endif
                    .Assembly;
                var versionAttr = assembly.GetCustomAttributes<AssemblyInformationalVersionAttribute>().SingleOrDefault();
                var formattedLogo = string.Format(logoStr, $"Version {versionAttr?.InformationalVersion ?? assembly.GetName().Version.ToString()}");
                logo = LineEndings.Split(formattedLogo);
            }
            else
            {
                logo = new string[0];
            }
        }

        /// <summary>
        /// Allows configuring resource providers for loading the application-host specific configuration.
        /// </summary>
        /// <param name="bundle">
        /// A reference to the <see cref="IConfigurableBundleContent"/> that is used to obtain the application-host
        /// configuration.
        /// </param>
        protected virtual void SetupConfigurationResourceBundle(IConfigurableBundleContent bundle)
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
        public string ApplicationConfigFileName { get; }

        /// <inheritdoc />
        public string HostConfigFileName { get; }

        /// <inheritdoc />
        public IConfiguration Configuration => _configuration;

        /// <inheritdoc />
        public string[] AsciiLogo => _logo;
    }
}