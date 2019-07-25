using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Axle.Configuration;
using Axle.DependencyInjection;
using Axle.Logging;
using Axle.Modularity;
using Axle.Verification;


namespace Axle
{
    public sealed partial class Application : IDisposable
    {
        public static IApplicationBuilder Build() => new Application();

        [Obsolete]
        private readonly string[] _args;

        private readonly object _syncRoot = new object();
        private readonly ModuleCatalogWrapper _moduleCatalog;
        private readonly IList<Type> _moduleTypes = new List<Type>();
        private readonly IList<Action<IContainer>> _onContainerReadyHandlers = new List<Action<IContainer>>();

        private ILoggingServiceProvider _loggingService;
        private IDependencyContainerProvider _dependencyContainerProvider;
        private volatile ModularContext _modularContext;
        private LayeredConfigManager _config;

        private Application()
        {
            _dependencyContainerProvider = new DefaultDependencyContainerProvider();
            _loggingService = new DefaultLoggingServiceProvider();
            //ModuleCatalog = new DefaultModuleCatalog();
            _moduleCatalog = new ModuleCatalogWrapper(new DefaultModuleCatalog());
            _config = new LayeredConfigManager();
        }

        [Obsolete]
        public Application(params string[] args) : this()
        {
            _args = args;
        }

        private void ThrowIfStarted()
        {
            lock (_syncRoot)
            if (_modularContext != null)
            {
                throw new InvalidOperationException("Application already started. ");
            }
        }
        private void ThrowIfNotStarted()
        {
            lock (_syncRoot)
            if (_modularContext == null)
            {
                throw new InvalidOperationException("Application not initialized yet. You must first call the Run method.");
            }
        }

        private ModularContext InitModularContext(IModuleCatalog c)
        {
            if (_modularContext != null)
            {
                return _modularContext;
            }
            lock (_syncRoot)
            if (_modularContext == null)
            {
                var ctx = new ModularContext(c, _dependencyContainerProvider, _loggingService, _config);
                ctx.Container.RegisterInstance(this);
                _modularContext = ctx;
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
            var ctx = InitModularContext(_moduleCatalog);
            foreach (var onContainerReadyHandler in _onContainerReadyHandlers)
            {
                onContainerReadyHandler.Invoke(ctx.Container);
            }
            ctx.Launch(_moduleTypes.ToArray()).Run(args);
            return this;
        }

        public void ShutDown()
        {
            lock (_syncRoot)
            {
                _modularContext?.Dispose();
            }
        }

        void IDisposable.Dispose() => ShutDown();

        [Obsolete]
        public Application Execute(params Assembly[] assemblies)
        {
            var c = _moduleCatalog;
            foreach (var type in assemblies.SelectMany(a => c.DiscoverModuleTypes(a)))
            {
                _moduleTypes.Add(type);
            }
            return Run(_args);
        }
        
        [Obsolete]
        public Application Execute(params Type[] types)
        {
            foreach (var type in types)
            {
                _moduleTypes.Add(type);
            }
            return Run(_args);
        }

        [Obsolete]
        public IDependencyContainerProvider DependencyContainerProvider
        {
            get => _dependencyContainerProvider;
            set => (this as IApplicationBuilder).SetDependencyContainerProvider(value.VerifyArgument(nameof(value)).IsNotNull().Value);
        }

        [Obsolete]
        public ILoggingServiceProvider LoggingService
        {
            get => _loggingService;
            set => (this as IApplicationBuilder).SetLoggingService(value.VerifyArgument(nameof(value)).IsNotNull().Value);
        }

        [Obsolete]
        public IContainer Container
        {
            get
            {
                ThrowIfNotStarted();
                return _modularContext?.Container;
            }
        }
    }
}
