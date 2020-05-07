using Axle.Configuration;
using Axle.DependencyInjection;
using Axle.Logging;

namespace Axle
{
    /// <summary>
    /// A class to serve as a base for application host implementations that need to be built upon an existing
    /// application host and to partially extend its features
    /// </summary>
    public abstract class ExtendingApplicationHost : AbstractApplicationHost
    {
        private readonly IApplicationHost _targetHost;

        protected ExtendingApplicationHost(
                IApplicationHost targetHost, 
                IDependencyContainerFactory dependencyContainerFactory,
                ILoggingService loggingService,
                string appConfigName,
                string hostConfigName,
                string environmentName) 
            : base(
                dependencyContainerFactory ?? targetHost.DependencyContainerFactory,
                loggingService ?? targetHost.LoggingService,
                appConfigName ?? targetHost.AppConfigFileName,
                hostConfigName ?? targetHost.HostConfigFileName,
                environmentName ?? targetHost.EnvironmentName)
        {
            _targetHost = targetHost;
        }

        /// <inheritdoc />
        public sealed override IConfiguration HostConfiguration
        {
            get
            {
                var baseCfg = base.HostConfiguration;
                if (baseCfg == null)
                {
                    return _targetHost.HostConfiguration;
                }
                return new LayeredConfigManager()
                    .Append(_targetHost.HostConfiguration)
                    .Append(baseCfg)
                    .LoadConfiguration()
                    ;
            }
        }
    }
}