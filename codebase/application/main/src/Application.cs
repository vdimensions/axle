using System;
using System.Collections.Generic;
using System.Linq;
using Axle.Configuration;
using Axle.DependencyInjection;
using Axle.Logging;
using Axle.Modularity;


namespace Axle
{
    public sealed partial class Application : IDisposable
    {
        public static IApplicationBuilder Build() => new Application();

        private readonly object _syncRoot = new object();
        private readonly ModuleCatalogWrapper _moduleCatalog = new ModuleCatalogWrapper(new DefaultModuleCatalog());
        private readonly IList<Type> _moduleTypes = new List<Type>();
        private readonly IList<Action<IContainer>> _onContainerReadyHandlers = new List<Action<IContainer>>();

        private LayeredConfigManager _config = new LayeredConfigManager();
        private ILoggingServiceProvider _loggingService;
        private IDependencyContainerProvider _dependencyContainerProvider;
        private volatile ModularContext _modularContext;

        private Application()
        {
            _dependencyContainerProvider = new DefaultDependencyContainerProvider();
            _loggingService = new DefaultLoggingServiceProvider();
        }

        private void ThrowIfStarted()
        {
            lock (_syncRoot)
            {
                if (_modularContext != null)
                {
                    throw new InvalidOperationException("Application already started. ");
                }
            }
        }
        private void ThrowIfNotStarted()
        {
            lock (_syncRoot)
            {
                if (_modularContext == null)
                {
                    throw new InvalidOperationException(
                        "Application not initialized yet. You must first call the Run method.");
                }
            }
        }

        private ModularContext InitModularContext(IConfigManager config, string[] args)
        {
            if (_modularContext != null)
            {
                return _modularContext;
            }
            lock (_syncRoot)
            {
                if (_modularContext == null)
                {
                    var ctx = new ModularContext(_dependencyContainerProvider, _loggingService, config, args);
                    ctx.Container.RegisterInstance(this);
                    _modularContext = ctx;
                }
            }
            return _modularContext;
        }

        private Application Load(IEnumerable<Type> types)
        {
            ThrowIfStarted();
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
            ThrowIfStarted();
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

            var ctx = InitModularContext(finalConfig, args);
            foreach (var onContainerReadyHandler in _onContainerReadyHandlers)
            {
                onContainerReadyHandler.Invoke(ctx.Container);
            }
            ctx.Launch(_moduleCatalog, finalConfig, _moduleTypes.ToArray()).Run();
            return this;
        }

        public void ShutDown()
        {
            lock (_syncRoot)
            {
                _modularContext?.Dispose();
            }
        }

        public IContainer CreateContainer() => _dependencyContainerProvider.Create();
        public IContainer CreateContainer(IContainer parent) => _dependencyContainerProvider.Create(parent);

        void IDisposable.Dispose() => ShutDown();
    }
}
