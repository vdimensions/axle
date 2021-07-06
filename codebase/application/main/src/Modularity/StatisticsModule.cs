using Axle.Logging;


namespace Axle.Modularity
{
    [Module]
    internal sealed class StatisticsModule
    {
        private readonly ILogger _logger;

        public StatisticsModule(ILogger logger)
        {
            _logger = logger;
        }

        // ReSharper disable UnusedMember.Local
        [ModuleDependencyInitialized]
        void DependencyInitialized(object module)
        {
        }

        [ModuleDependencyTerminated]
        void DependencyTerminated(object module)
        {
        }
        // ReSharper restore UnusedMember.Local
    }
}