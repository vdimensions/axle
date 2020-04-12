using System;
using System.Collections.Generic;
using Axle.Configuration;
using Axle.DependencyInjection;
using Axle.Logging;
using Axle.Modularity;

namespace Axle
{
    /*
    internal sealed class ApplicationContainer
    {
        private Application _app;

        private readonly IDependencyContainerFactory _dependencyContainerFactory;
        private readonly IDependencyContext _rootContext;
        private readonly ILoggingService _loggingService;

        public ApplicationContainer(
            IDependencyContainerFactory dependencyContainerFactory, 
            IDependencyContext rootContext, 
            ILoggingService loggingService)
        {
            _rootContext = rootContext;
            _loggingService = loggingService;
            _dependencyContainerFactory = new ApplicationContainerFactory(dependencyContainerFactory, _rootContext);
        }


        [ModuleEntryPoint]
        public void Run()
        {
            var aggregatingLoggingService = new AggregatingLoggingService(_loggingService == null ? new ILoggingService[0] : new[]{_loggingService});
            var host = new AxleApplicationHost(_dependencyContainerFactory, aggregatingLoggingService);
            try
            {
                var rootContainer = host.DependencyContainerFactory.CreateContainer();
                rootContainer
                    .Export(aggregatingLoggingService)
                    .Export(new ApplicationContainerFactory(host.DependencyContainerFactory, rootContainer))
                    .Export(host);
                
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
                
                foreach (var onContainerReadyHandler in _onContainerReadyHandlers)
                {
                    onContainerReadyHandler.Invoke(rootContainer);
                }
                // TODO: remove the host usage here
                var app = Application.Launch(_moduleCatalog, _moduleTypes, host, rootContainer, finalConfig, args);
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
    */
    
    partial class Application
    {
        private sealed partial class Builder
        {
            private readonly object _syncRoot = new object();
            private readonly ApplicationModuleCatalog _moduleCatalog = new ApplicationModuleCatalog(new DefaultModuleCatalog());
            private readonly IList<Type> _moduleTypes = new List<Type>();
            private readonly IList<Action<IDependencyContainer>> _onContainerReadyHandlers = new List<Action<IDependencyContainer>>();

            private LayeredConfigManager _config = new LayeredConfigManager();
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
                var host = new AxleApplicationHost(_dependencyContainerFactory, aggregatingLoggingService);
                try
                {
                    var rootContainer = host.DependencyContainerFactory.CreateContainer();
                    rootContainer
                        .Export(aggregatingLoggingService)
                        .Export(new ApplicationContainerFactory(host.DependencyContainerFactory, rootContainer))
                        .Export(host);
                    
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
                    
                    foreach (var onContainerReadyHandler in _onContainerReadyHandlers)
                    {
                        onContainerReadyHandler.Invoke(rootContainer);
                    }
                    // TODO: remove the host usage here
                    var app = Launch(_moduleCatalog, _moduleTypes, host, rootContainer, finalConfig, args);
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
