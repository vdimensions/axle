using Axle.Application.Modularity;
using Axle.Core;


namespace Axle.Application
{
    public class App
    {
        public static void Run(params string[] args)
        {
            // 1. Discover infrastructure component candidates
            // 2. Prepare infrastructure providers (logging, configuration, DI)
            // 3. Initialize modularity
        }

        public App(ILoggingServiceProvider loggingService, IDependencyContainerProvider dependencyContainerProvider, IReflectionProvider reflectionProvider)
        {
            LoggingService = loggingService;
            DependencyContainerProvider = dependencyContainerProvider;
            ReflectionProvider = reflectionProvider;
        }

        public ILoggingServiceProvider LoggingService { get; }
        public IDependencyContainerProvider DependencyContainerProvider { get; }
        public IReflectionProvider ReflectionProvider { get; }
        // TODO: configuration provider
    }
}
