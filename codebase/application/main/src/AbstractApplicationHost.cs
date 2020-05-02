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
using Axle.Resources.Extraction;
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
        {
            DependencyContainerFactory = dependencyContainerFactory ?? new AxleDependencyContainerFactory();
            LoggingService = new AggregatingLoggingService();
            if (loggingService != null)
            {
                ((ILoggingServiceRegistry) LoggingService).RegisterLoggingService(loggingService);
            }
            EnvironmentName = environmentName;
            LoadConfiguration(out _configuration, out _logo, EnvironmentName, profiles);
        }
        
        private void LoadConfiguration(
            out IConfiguration config, 
            out string[] logo, 
            string environmentName, 
            params string[] profiles)
        {
            var assembly = typeof(DefaultApplicationHost)
                #if NETSTANDARD || NET45_OR_NEWER
                .GetTypeInfo()
                #endif
                .Assembly;
            var asmName = assembly.GetName();
            var resourceManager = new DefaultResourceManager();
            SetupConfigurationResourceBundle(resourceManager.Bundles.Configure(Application.ConfigBundleName));
            var configStreamProvider = new ResourceConfigurationStreamProvider(resourceManager);
            
            var tmpConfig = Application.Configure(new LayeredConfigManager(), configStreamProvider, string.Empty);
            foreach (var profile in profiles)
            {
                tmpConfig = Application.Configure(tmpConfig, configStreamProvider, profile);
            }
            tmpConfig = Application.Configure(tmpConfig, configStreamProvider, environmentName);
            config = tmpConfig.LoadConfiguration();
            
            var logoStr = resourceManager.Load<string>(Application.ConfigBundleName, "logo.txt", CultureInfo.InvariantCulture);
            if (logoStr != null)
            {
                var versionAttr = assembly.GetCustomAttributes<AssemblyInformationalVersionAttribute>().SingleOrDefault();
                var formattedLogo = string.Format(logoStr, $"Version {versionAttr?.InformationalVersion ?? asmName.Version.ToString()}");
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
            bundle.Extractors.Register(new PathForwardingResourceExtractor("Host"));
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
        public void Dispose() => Dispose(true);
        void IDisposable.Dispose() => Dispose(true);

        public IDependencyContainerFactory DependencyContainerFactory { get; }
        public ILoggingService LoggingService { get; }
        public string EnvironmentName { get; }
        public IConfiguration Configuration => _configuration;
        public string[] Logo => _logo;
    }
}