#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
#if NETSTANDARD || NET45_OR_NEWER
using System.Reflection;
#endif
using Axle.Configuration;
using Axle.DependencyInjection;
using Axle.Logging;
using Axle.References;
using Axle.Resources;
#if NET35_OR_NEWER && !NET45_OR_NEWER && !NETSTANDARD
using Axle.Reflection.Extensions.Assembly;
#endif
using Axle.Text;

namespace Axle
{
    /// <summary>
    /// The default <see cref="IApplicationHost"/> implementation. 
    /// </summary>
    public sealed class DefaultApplicationHost : IApplicationHost, IDisposable
    {
        private static void LoadConfiguration(string environmentName, out IConfiguration config, out string[] logo)
        {
            var assembly = typeof(DefaultApplicationHost)
                #if NETSTANDARD || NET45_OR_NEWER
                .GetTypeInfo()
                #endif
                .Assembly;
            var asmName = assembly.GetName();
            var resourceManager = new DefaultResourceManager();
            resourceManager.Bundles.Configure(Application.ConfigBundleName).Register(new Uri($"assembly://{asmName.Name}", UriKind.Absolute));
            
            config = Application.Configure(
                    Application.Configure(new LayeredConfigManager(), x => Application.LoadResourceConfig(resourceManager, "Host", x), string.Empty),
                    x => Application.LoadResourceConfig(resourceManager, "Host", x),
                    environmentName)
                .LoadConfiguration();
            
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

        private static string InferredEnvironmentName
        {
            get
            {
                #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
                var dotnetEnv = System.Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT", EnvironmentVariableTarget.Process);
                #else
                var dotnetEnv = System.Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
                #endif
                if (!string.IsNullOrEmpty(dotnetEnv))
                {
                    return dotnetEnv;
                }
                #if DEBUG
                return "Debug";
                #endif
                return string.Empty;
            }
        }
        public static DefaultApplicationHost Instance => Singleton<DefaultApplicationHost>.Instance;
        
        private readonly IConfiguration _configuration;
        private readonly string[] _logo;
        
        private DefaultApplicationHost(
                IDependencyContainerFactory dependencyContainerFactory, 
                ILoggingService loggingService, 
                string environmentName)
        {
            DependencyContainerFactory = dependencyContainerFactory ?? new AxleDependencyContainerFactory();
            LoggingService = loggingService ?? new AggregatingLoggingService();
            LoadConfiguration(EnvironmentName = environmentName, out _configuration, out _logo);
        }

        private DefaultApplicationHost(
                IDependencyContainerFactory dependencyContainerFactory, 
                ILoggingService loggingService) 
            : this(dependencyContainerFactory, loggingService, InferredEnvironmentName) { }
        
        private DefaultApplicationHost() : this(null, null) { }

        public void Dispose()
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

        public IDependencyContainerFactory DependencyContainerFactory { get; }
        public ILoggingService LoggingService { get; }
        public string EnvironmentName { get; }
        public IConfiguration Configuration => _configuration;
        public string[] Logo => _logo;
    }
}
#endif