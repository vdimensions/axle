using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Logging
{
    [Module]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal sealed class LoggingModule
    {
        private readonly AggregatingLoggingService _loggingService;

        public LoggingModule(AggregatingLoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        [ModuleDependencyInitialized]
        internal void DependencyInitialized(ILoggingServiceConfigurer loggingServiceConfigurer)
        {
            loggingServiceConfigurer.Configure(_loggingService);
        }

        [ModuleEntryPoint]
        internal void Run()
        {
            _loggingService.FlushMessages();
        }
    }
}
