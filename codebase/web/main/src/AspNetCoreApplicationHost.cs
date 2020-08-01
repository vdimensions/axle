using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using Axle.DependencyInjection;
using Axle.Environment;
using Axle.References;
using Axle.Resources.Bundling;
using Microsoft.Extensions.DependencyInjection;

namespace Axle.Web.AspNetCore
{
    /// <summary>
    /// The <see cref="IApplicationHost"/> implementation designed for use with aspnetcore.
    /// </summary>
    public sealed class AspNetCoreApplicationHost : ExtendingApplicationHost, IServiceConfigurer
    {
        /// <summary>
        /// Gets the sole instance of the <see cref="AspNetCoreApplicationHost"/> class.
        /// </summary>
        public static AspNetCoreApplicationHost Instance => Singleton<AspNetCoreApplicationHost>.Instance.Value;

        private readonly ConcurrentDictionary<Type, object> _exportedObjects;

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private AspNetCoreApplicationHost() : this(DefaultApplicationHost.Instance.DependencyContainerFactory) { }
        private AspNetCoreApplicationHost(IDependencyContainerFactory dependencyContainerFactory) : this(
            dependencyContainerFactory is AspNetCoreDependencyContainerFactory aspNetCoreDependencyContainerFactory 
                ? aspNetCoreDependencyContainerFactory 
                : new AspNetCoreDependencyContainerFactory(dependencyContainerFactory)) { }
        private AspNetCoreApplicationHost(AspNetCoreDependencyContainerFactory dependencyContainerFactory) : base(
            DefaultApplicationHost.Instance, 
            dependencyContainerFactory,
            null,
            null,
            null,
            Platform.Environment["ASPNETCORE_ENVIRONMENT"])
        {
            _exportedObjects = dependencyContainerFactory.ExportedObjects;
        }

        /// <inheritdoc />
        public void Configure(IServiceCollection services)
        {
            foreach (var pair in _exportedObjects.ToArray())
            {
                services.AddSingleton(pair.Key, pair.Value);
            }
        }

        /// <inheritdoc />
        protected override void SetupAppConfigurationResourceBundle(IConfigurableBundleContent bundle)
        {
            base.SetupAppConfigurationResourceBundle(bundle.Register(new Uri("./", UriKind.Relative)));
        }

        /// <inheritdoc />
        protected override void SetupHostConfigurationResourceBundle(IConfigurableBundleContent bundle)
        {
            base.SetupHostConfigurationResourceBundle(bundle.Register(new Uri("./", UriKind.Relative)));
        }
    }
}