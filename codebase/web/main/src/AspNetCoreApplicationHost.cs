using System;
using System.Collections.Concurrent;
using Axle.Configuration;
using Axle.DependencyInjection;
using Axle.Logging;
using Axle.References;
using Microsoft.Extensions.DependencyInjection;

namespace Axle.Web.AspNetCore
{
    /// <summary>
    /// The <see cref="IApplicationHost"/> implementation designed for use with aspnetcore.
    /// </summary>
    public sealed class AspNetCoreApplicationHost : IApplicationHost, IServiceConfigurer
    {
        public static AspNetCoreApplicationHost Instance => Singleton<AspNetCoreApplicationHost>.Instance.Value;
        
        private readonly string _environmentName;
        private readonly ILoggingService _loggingService;
        private readonly IDependencyContainerFactory _dependencyContainerFactory;
        private readonly IConfiguration _configuration;
        private readonly string[] _logo;
        private readonly ConcurrentDictionary<Type, object> _exportedObjects = new ConcurrentDictionary<Type, object>();
        
        private AspNetCoreApplicationHost()
        {
            var defaultAppHost = DefaultApplicationHost.Instance;
            
            _dependencyContainerFactory = new AspNetCoreDependencyContainerFactory(defaultAppHost.DependencyContainerFactory, _exportedObjects);
            
            _loggingService = defaultAppHost.LoggingService;
            
            _environmentName = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", EnvironmentVariableTarget.Process);
            if (string.IsNullOrEmpty(_environmentName))
            {
                _environmentName = defaultAppHost.EnvironmentName; 
            }

            _configuration = defaultAppHost.Configuration;
            _logo = defaultAppHost.Logo;
        }
        
        public void Configure(IServiceCollection services)
        {
            foreach (var pair in _exportedObjects.ToArray())
            {
                services.AddSingleton(pair.Key, pair.Value);
            }
        }

        IDependencyContainerFactory IApplicationHost.DependencyContainerFactory => _dependencyContainerFactory;
        ILoggingService IApplicationHost.LoggingService => _loggingService;
        string IApplicationHost.EnvironmentName => _environmentName;
        IConfiguration IApplicationHost.Configuration => _configuration;
        string[] IApplicationHost.Logo => _logo;
    }
}