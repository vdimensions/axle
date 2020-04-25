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
            private readonly IModuleCatalog _moduleCatalog = new DefaultModuleCatalog();
            private readonly IList<Type> _moduleTypes = new List<Type>();
            private readonly IList<Action<IDependencyContainer>> _onContainerReadyHandlers = new List<Action<IDependencyContainer>>();

            private LayeredConfigManager _config = new LayeredConfigManager();
            private IApplicationHost _host = new DefaultApplicationHost();
            private ILoggingService _loggingService;
            private IDependencyContainerFactory _dependencyContainerFactory = new AxleDependencyContainerFactory();

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
                var aggregatingLoggingService = new AggregatingLoggingService(_loggingService == null ? new ILoggingService[0] : new[]{_loggingService});
                var hostContainer = _host.DependencyContainerFactory.CreateContainer();
                hostContainer
                    .Export(_dependencyContainerFactory)
                    .Export(aggregatingLoggingService);
                var rootM = new[]
                {
                    typeof(ConfigSourceRegistry),
                    typeof(LoggingModule)
                };
                
                try 
                {
                    var rootContainer = _host.DependencyContainerFactory.CreateContainer(hostContainer);
                    rootContainer
                        //.Export(aggregatingLoggingService)
                        .Export(new ApplicationContainerFactory(_host.DependencyContainerFactory, rootContainer))
                        .Export(_host);
                    
                    var config = _config
                        .Append(EnvironmentConfigSource.Instance)
                        //TODO: .Append() application.xxx
                        ;            
                    var modulesConfigSection = config
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
                    
                    foreach (var onContainerReadyHandler in _onContainerReadyHandlers)
                    {
                        onContainerReadyHandler.Invoke(rootContainer);
                    }
                    // TODO: remove the host usage here
                    var app = Launch(
                        new ApplicationModuleCatalog(_moduleCatalog), _moduleTypes, _host, rootContainer, config, args);
                    app.Run();
                    return app;
                }
                catch (Exception)
                {
                    if (_host is IDisposable d)
                    {
                        d.Dispose();
                    }
                    throw;
                }
            }
        }
    }
}
