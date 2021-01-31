using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Logging
{
    [Module]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal sealed class LoggingModule
    {
        private readonly MutableLoggingService _loggingService;

        public LoggingModule(MutableLoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        [ModuleDependencyInitialized]
        internal void DependencyInitialized(ILoggingServiceConfigurer loggingServiceConfigurer)
        {
            loggingServiceConfigurer.Configure(_loggingService);
        }
    }
}
