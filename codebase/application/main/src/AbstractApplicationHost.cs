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
            LoggingService = loggingService ?? new AggregatingLoggingService();
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
            
            var tmpConfig = Application.Configure(new LayeredConfigManager(), x => Application.LoadResourceConfig(resourceManager, x), string.Empty);
            foreach (var profile in profiles)
            {
                tmpConfig = Application.Configure(
                    tmpConfig,
                    x => Application.LoadResourceConfig(resourceManager, x),
                    profile);
            }
            tmpConfig = Application.Configure(tmpConfig, x => Application.LoadResourceConfig(resourceManager, x), environmentName);
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

        protected virtual void SetupConfigurationResourceBundle(IConfigurableBundleContent resourceManager)
        {
            resourceManager.Extractors.Register(new PathForwardingResourceExtractor("Host"));
        }
        
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