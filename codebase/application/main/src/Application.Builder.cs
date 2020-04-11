using System;
using System.Collections.Generic;
using System.Linq;
using Axle.Configuration;
using Axle.DependencyInjection;
using Axle.Logging;
using Axle.Modularity;

namespace Axle
{
    partial class Application
    {
        private sealed partial class Builder
        {
            private readonly object _syncRoot = new object();
            private readonly ModuleCatalogWrapper _moduleCatalog = new ModuleCatalogWrapper(new DefaultModuleCatalog());
            private readonly IList<Type> _moduleTypes = new List<Type>();
            private readonly IList<Action<IDependencyContainer>> _onContainerReadyHandlers = new List<Action<IDependencyContainer>>();

            private LayeredConfigManager _config = new LayeredConfigManager();
            private ILoggingService _loggingService;
            private IDependencyContainerFactory _dependencyContainerFactory;
            private volatile IApplicationHost _host;

            private Builder Load(IEnumerable<Type> types)
            {
                foreach (var type in types)
                {
                    if (type != null)
                    {
                        _moduleTypes.Add(type);
                    }
                }
                return this;
            }

            public Application Run(params string[] args)
            {
                var finalConfig = _config.Append(EnvironmentConfigSource.Instance);            
                var modulesConfigSection = finalConfig
                    .LoadConfiguration()
                    .GetSection("axle")?.GetSection("application")?.GetSection("modules");
                var includedModules = modulesConfigSection?.GetSection("include");
                var excludedModules = modulesConfigSection?.GetSection("exclude");
                if (includedModules != null)
                {
                    // TODO: add modules to _moduleTypes
                }
                if (excludedModules != null)
                {
                    // TODO: remove excluded modules from _moduleTypes
                }

                var loadedModules = _moduleCatalog.GetModules(
                    _moduleTypes.ToArray(),
                    _host?.GetType(), 
                    args);
                
                var rankedModules = _moduleCatalog
                    .RankModules(loadedModules)
                    .ToArray();

                var aggregatingLoggingService = new AggregatingLoggingService(_loggingService == null ? new ILoggingService[0] : new[]{_loggingService});
                var host = new AxleApplicationHost(_dependencyContainerFactory, aggregatingLoggingService);
                try
                {
                    var rootContainer = host.DependencyContainerFactory.CreateContainer();
                    rootContainer
                        .Export(aggregatingLoggingService)
                        .Export(host.DependencyContainerFactory)
                        .Export(host);
                    foreach (var onContainerReadyHandler in _onContainerReadyHandlers)
                    {
                        onContainerReadyHandler.Invoke(rootContainer);
                    }
                    var app = new Application(rankedModules, host, rootContainer, finalConfig, args);
                    aggregatingLoggingService.FlushMessages();
                    app.Run();
                    return app;
                }
                catch (Exception)
                {
                    host.Dispose();
                    throw;
                }
            }
        }
    }
}
