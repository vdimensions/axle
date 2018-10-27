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
    public sealed class Application : IDisposable
    {
        private readonly object _syncRoot = new object();

        private readonly string[] _args;

        private ILoggingServiceProvider _loggingService;
        private IDependencyContainerProvider _dependencyContainerProvider;
        private ModuleCatalogWrapper _moduleCatalog;
        private volatile ModularContext _modularContext;
        private readonly IList<Type> _moduleTypes = new List<Type>();

        public Application(params string[] args)
        {
            ModuleCatalog = new DefaultModuleCatalog();
            DependencyContainerProvider = new DefaultDependencyContainerProvider();
            LoggingService = new DefaultLoggingServiceProvider();
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

        public Application Load(Type type)
        {
            ThrowIfStarted();
            _moduleTypes.Add(type.VerifyArgument(nameof(type)).IsNotNull());
            return this;
        }

        public Application Load(Assembly assembly)
        {
            ThrowIfStarted();
            foreach (var type in _moduleCatalog.DiscoverModuleTypes(assembly.VerifyArgument(nameof(assembly)).IsNotNull()))
            {
                _moduleTypes.Add(type);
            }
            return this;
        }

        public Application Run(params string[] args)
        {
            ThrowIfStarted();
            InitModularContext(_moduleCatalog).Launch(_moduleTypes.ToArray()).Run(args);
            return this;
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

        public void ShutDown()
        {
            lock (_syncRoot)
            {
                _modularContext?.Dispose();
            }
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

        public IDependencyContainerProvider DependencyContainerProvider
        {
            get => _dependencyContainerProvider;
            set
            {
                lock (_syncRoot)
                {
                    ThrowIfStarted();
                    _dependencyContainerProvider = value.VerifyArgument(nameof(value)).IsNotNull().Value;
                }
            }
        }

        public ILoggingServiceProvider LoggingService
        {
            get => _loggingService;
            set
            {
                lock (_syncRoot)
                {
                    ThrowIfStarted();
                    _loggingService = value.VerifyArgument(nameof(value)).IsNotNull().Value;
                }
            }
        }

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
