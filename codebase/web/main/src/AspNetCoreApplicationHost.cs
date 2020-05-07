using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using Axle.Configuration;
using Axle.References;
using Axle.Resources.Bundling;
using Microsoft.Extensions.DependencyInjection;

namespace Axle.Web.AspNetCore
{
    /// <summary>
    /// The <see cref="IApplicationHost"/> implementation designed for use with aspnetcore.
    /// </summary>
    public sealed class AspNetCoreApplicationHost : AbstractApplicationHost, IServiceConfigurer
    {
        public static AspNetCoreApplicationHost Instance => Singleton<AspNetCoreApplicationHost>.Instance.Value;

        private readonly ConcurrentDictionary<Type, object> _exportedObjects;

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private AspNetCoreApplicationHost() 
            : this(new AspNetCoreDependencyContainerFactory(DefaultApplicationHost.Instance.DependencyContainerFactory))
        { }
        private AspNetCoreApplicationHost(AspNetCoreDependencyContainerFactory dependencyContainerFactory) : base(
            dependencyContainerFactory,
            DefaultApplicationHost.Instance.LoggingService,
            DefaultApplicationHost.Instance.AppConfigFileName,
            DefaultApplicationHost.Instance.HostConfigFileName,
            System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", EnvironmentVariableTarget.Process) ??
            DefaultApplicationHost.Instance.EnvironmentName)
        {
            _exportedObjects = dependencyContainerFactory.ExportedObjects;
            
        }
        
        public void Configure(IServiceCollection services)
        {
            foreach (var pair in _exportedObjects.ToArray())
            {
                services.AddSingleton(pair.Key, pair.Value);
            }
        }

        protected override void SetupAppConfigurationResourceBundle(IConfigurableBundleContent bundle)
        {
            base.SetupAppConfigurationResourceBundle(bundle.Register(new Uri("./", UriKind.Relative)));
        }
        protected override void SetupHostConfigurationResourceBundle(IConfigurableBundleContent bundle)
        {
            base.SetupAppConfigurationResourceBundle(bundle.Register(new Uri("./", UriKind.Relative)));
        }

        public override IConfiguration HostConfiguration
        {
            get
            {
                var baseCfg = base.HostConfiguration;
                if (baseCfg == null)
                {
                    return DefaultApplicationHost.Instance.HostConfiguration;
                }
                return new LayeredConfigManager()
                    .Append(baseCfg)
                    .Append(DefaultApplicationHost.Instance.HostConfiguration)
                    .LoadConfiguration()
                    ;
            }
        }
    }
}