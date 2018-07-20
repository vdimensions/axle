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
            _logger.Trace("Module `{0}` loaded", module);
        }

        [ModuleDependencyTerminated]
        void DependencyTerminated(object module)
        {
            _logger.Trace("Module `{0}` terminated", module);
        }
        // ReSharper restore UnusedMember.Local
    }
}