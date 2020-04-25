#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
using System;
using Axle.DependencyInjection;
using Axle.Logging;

namespace Axle
{
    /// <summary>
    /// The default <see cref="IApplicationHost"/> implementation. 
    /// </summary>
    public sealed class DefaultApplicationHost : IApplicationHost, IDisposable
    {
        private static string InferredEnvironmentName
        {
            get
            {
                var dotnetEnv = System.Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT", EnvironmentVariableTarget.Process);
                if (!string.IsNullOrEmpty(dotnetEnv))
                {
                    return dotnetEnv;
                }
                #if DEBUG
                return "Debug";
                #endif
                return string.Empty;
            }
        }
        
        public DefaultApplicationHost(
                IDependencyContainerFactory dependencyContainerFactory, 
                ILoggingService loggingService, 
                string environmentName)
        {
            DependencyContainerFactory = dependencyContainerFactory ?? new AxleDependencyContainerFactory();
            LoggingService = loggingService ?? new AggregatingLoggingService();
            EnvironmentName = environmentName;
        }
        public DefaultApplicationHost(
                IDependencyContainerFactory dependencyContainerFactory, 
                ILoggingService loggingService) 
            : this(dependencyContainerFactory, loggingService, InferredEnvironmentName) { }
        
        public DefaultApplicationHost() : this(new AxleDependencyContainerFactory(), new AxleLoggingService()) { }

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