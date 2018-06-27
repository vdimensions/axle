using System;

using Axle.Application.DependencyInjection;
using Axle.Application.Logging;
using Axle.Application.Modularity;
using Axle.Verification;


namespace Axle.Application
{
    public sealed class App : IDisposable
    {
        private readonly object _syncRoot = new object();

        private ILoggingServiceProvider _loggingService;
        private IDependencyContainerProvider _dependencyContainerProvider;
        private IModuleCatalog _moduleCatalog;
        private IContainer _appContainer;

        public App()
        {
            LoggingService = new DefaultLoggingServiceProvider();
            DependencyContainerProvider = new DefaultDependencyContainerProvider();
            ModuleCatalog = new DefaultModuleCatalog();
        }

        public void Run(params string[] args)
        {
            lock (_syncRoot)
            {
                if (_appContainer == null)
                {
                    _appContainer = Modules.Launch(ModuleCatalog, DependencyContainerProvider);
                }
            }
        }

        private void CheckIfStarted()
        {
            lock (_syncRoot)
                if (_appContainer != null)
                {
                    throw new InvalidOperationException("Application already started");
                }
        }

        public void ShutDown()
        {
            lock (_syncRoot)
            {
                _appContainer?.Dispose();
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
    }
}
