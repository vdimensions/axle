using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Axle.DependencyInjection;
using Axle.Logging;
using Axle.Modularity;
using Axle.Verification;


namespace Axle
{
    public interface IApplicationBuilder
    {
        IApplicationBuilder SetDependencyContainerProvider(IDependencyContainerProvider containerProvider);
        IApplicationBuilder SetLoggingService(ILoggingServiceProvider loggingService);
        IApplicationBuilder ConfigureDependencies(Action<IContainer> configAction);
        IApplicationBuilder Load(Type type);
        IApplicationBuilder Load(params Type[] types);
        IApplicationBuilder Load(Assembly assembly);
        IApplicationBuilder Load(IEnumerable<Assembly> assemblies);
        Application Run(params string[] args);
    }
    public sealed class Application : IApplicationBuilder, IDisposable
    {
        public static IApplicationBuilder Build() => new Application();

        private readonly object _syncRoot = new object();

        [Obsolete]
        private readonly string[] _args;

        private ILoggingServiceProvider _loggingService;
        private IDependencyContainerProvider _dependencyContainerProvider;
        private ModuleCatalogWrapper _moduleCatalog;
        private volatile ModularContext _modularContext;
        private readonly IList<Type> _moduleTypes = new List<Type>();
        private readonly IList<Action<IContainer>> _onContainerReadyHandlers = new List<Action<IContainer>>();

        private Application()
        {
            ModuleCatalog = new DefaultModuleCatalog();
            DependencyContainerProvider = new DefaultDependencyContainerProvider();
            LoggingService = new DefaultLoggingServiceProvider();
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
                throw new InvalidOperationException("Application not initialized yet. You must first call the Execute method.");
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
                var ctx = new ModularContext(c, _dependencyContainerProvider, _loggingService);
                ctx.Container.RegisterInstance(this);
                _modularContext = ctx;
            }
            return _modularContext;
        }

        public IApplicationBuilder SetDependencyContainerProvider(IDependencyContainerProvider containerProvider)
        {
            lock (_syncRoot)
            {
                ThrowIfStarted();
                _dependencyContainerProvider = containerProvider;
            }
            return this;
        }

        public IApplicationBuilder SetLoggingService(ILoggingServiceProvider loggingService)
        {
            lock (_syncRoot)
            {
                ThrowIfStarted();
                _loggingService = loggingService;
            }
            return this;
        }

        public IApplicationBuilder ConfigureDependencies(Action<IContainer> configAction)
        {
            ThrowIfStarted();
            configAction.VerifyArgument(nameof(configAction)).IsNotNull();
            _onContainerReadyHandlers.Add(configAction);
            return this;
        }

        public IApplicationBuilder Load(Type type)
        {
            ThrowIfStarted();
            _moduleTypes.Add(type.VerifyArgument(nameof(type)).IsNotNull());
            return this;
        }
        public IApplicationBuilder Load(params Type[] types)
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
        public IApplicationBuilder Load(Assembly assembly)
        {
            ThrowIfStarted();
            foreach (var type in _moduleCatalog.DiscoverModuleTypes(assembly.VerifyArgument(nameof(assembly)).IsNotNull()))
            {
                _moduleTypes.Add(type);
            }
            return this;
        }
        public IApplicationBuilder Load(IEnumerable<Assembly> assemblies)
        {
            ThrowIfStarted();
            foreach (var type in assemblies.SelectMany(a => _moduleCatalog.DiscoverModuleTypes(a)))
            {
                _moduleTypes.Add(type);
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

        void IDisposable.Dispose() => ShutDown();

        private IModuleCatalog ModuleCatalog
        {
            get => _moduleCatalog;
            set
            {
                lock (_syncRoot)
                {
                    ThrowIfStarted();
                    _moduleCatalog = new ModuleCatalogWrapper(value.VerifyArgument(nameof(value)).IsNotNull().Value);
                }
            }
        }

        [Obsolete]
        public IDependencyContainerProvider DependencyContainerProvider
        {
            get => _dependencyContainerProvider;
            set => SetDependencyContainerProvider(value.VerifyArgument(nameof(value)).IsNotNull().Value);
        }

        [Obsolete]
        public ILoggingServiceProvider LoggingService
        {
            get => _loggingService;
            set => SetLoggingService(value.VerifyArgument(nameof(value)).IsNotNull().Value);
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
