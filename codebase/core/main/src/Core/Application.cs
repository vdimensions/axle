using Axle.Core.Modularity;


namespace Axle.Core
{
    public class Application
    {
        public static void Run(params string[] args)
        {
            // 1. Discover infrastructure component candidates
            // 2. Prepare infrastructure providers (logging, configuration, DI)
            // 3. Initialize modularity
        }

        public Application(ILoggingServiceProvider loggingService, IDependencyContainerProvider dependencyContainerProvider, IReflectionProvider reflectionProvider)
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
