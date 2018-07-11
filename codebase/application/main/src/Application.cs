using System;
using System.Collections.Generic;
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

        private ILoggingServiceProvider _loggingService;
        private IDependencyContainerProvider _dependencyContainerProvider;
        private IModuleCatalog _moduleCatalog;
        private ModularContext _modularContext;
        private readonly string[] _args;


        public Application(params string[] args)
        {
            LoggingService = new DefaultLoggingServiceProvider();
            DependencyContainerProvider = new DefaultDependencyContainerProvider();
            ModuleCatalog = new DefaultModuleCatalog();
            _args = args;
        }

        public Application Execute(params Assembly[] assemblies)
        {
            var c = _moduleCatalog;
            if (_modularContext == null)
            {
                lock (_syncRoot)
                {
                    if (_modularContext == null)
                    {
                        _modularContext = new ModularContext(c, _dependencyContainerProvider, _loggingService);
                    }
                }
            }

            var types = new List<Type>();
            for (var i = 0; i < assemblies.Length; i++)
            {
                var assemblyTypes = c.DiscoverModuleTypes(assemblies[i]);
                for (var j = 0; j < assemblyTypes.Length; j++)
                {
                    types.Add(assemblyTypes[j]);
                }
            }

            _modularContext.Launch(types.ToArray()).Run(_args);
            return this;
        }

        public Application Execute(params Type[] types)
        {
            if (_modularContext == null)
            {
                lock (_syncRoot)
                {
                    if (_modularContext == null)
                    {
                        _modularContext = new ModularContext(_moduleCatalog, _dependencyContainerProvider, _loggingService);
                    }
                }
            }

            //#if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
            //_modularContext.Launch(_moduleCatalog.DiscoverModuleTypes());
            //#endif
            _modularContext.Launch(types).Run(_args);
            return this;
        }

        private void CheckIfStarted()
        {
            lock (_syncRoot)
            if (_modularContext != null)
            {
                throw new InvalidOperationException("Application already started");
            }
        }

        public void ShutDown()
        {
            lock (_syncRoot)
            {
                _modularContext?.Dispose();
            }
        }

        void IDisposable.Dispose() => ShutDown();

        public ILoggingServiceProvider LoggingService
        {
            get => _loggingService;
            set
            {
                lock (_syncRoot)
                {
                    CheckIfStarted();
                    _loggingService = value.VerifyArgument(nameof(value)).IsNotNull().Value;
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
                    CheckIfStarted();
                    _dependencyContainerProvider = value.VerifyArgument(nameof(value)).IsNotNull().Value;
                }
            }
        }

        public IModuleCatalog ModuleCatalog
        {
            get => _moduleCatalog;
            set
            {
                lock (_syncRoot)
                {
                    CheckIfStarted();
                    _moduleCatalog = value.VerifyArgument(nameof(value)).IsNotNull().Value;
                }
            }
        }

        public IContainer Container => _modularContext?.Container;
    }
}
