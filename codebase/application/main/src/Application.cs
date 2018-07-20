using System;
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

        public Application Execute(params Assembly[] assemblies)
        {
            var c = _moduleCatalog;
            var types = assemblies.SelectMany(a => c.DiscoverModuleTypes(a)).ToArray();
            InitModularContext(c).Launch(types).Run(_args);
            return this;
        }

        public Application Execute(params Type[] types)
        {
            InitModularContext(_moduleCatalog).Launch(types).Run(_args);
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
