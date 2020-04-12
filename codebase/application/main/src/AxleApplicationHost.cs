#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
using System;
using Axle.DependencyInjection;
using Axle.Logging;

namespace Axle
{
    internal sealed class AxleApplicationHost : IApplicationHost, IDisposable
    {
        private AxleApplicationHost(
                IDependencyContainerFactory dependencyContainerFactory, 
                ILoggingService loggingService, 
                string environmentName)
        {
            DependencyContainerFactory = dependencyContainerFactory ?? new AxleDependencyContainerFactory();
            LoggingService = loggingService ?? new AxleLoggingService();
            EnvironmentName = environmentName;
        }
        public AxleApplicationHost(
                IDependencyContainerFactory dependencyContainerFactory, 
                ILoggingService loggingService) 
            : this(
                dependencyContainerFactory,
                loggingService,
                #if DEBUG
                "development"
                #else
                string.Empty
                #endif
                ) { }
        
        public AxleApplicationHost() : this(new AxleDependencyContainerFactory(), new AxleLoggingService()) { }

        public void Dispose()
        {
            if (LoggingService is IDisposable disposableLoggingServiceProvider)
            {
                disposableLoggingServiceProvider.Dispose();
            }
            if (DependencyContainerFactory is IDisposable disposableDependencyContainerProvider)
            {
                disposableDependencyContainerProvider.Dispose();
            }
        }

        public IDependencyContainerFactory DependencyContainerFactory { get; }
        public ILoggingService LoggingService { get; }
        public string EnvironmentName { get; }
    }
}
#endif