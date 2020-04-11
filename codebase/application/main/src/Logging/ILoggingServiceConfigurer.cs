using Axle.Modularity;

namespace Axle.Logging
{
    [Requires(typeof(LoggingModule))]
    public interface ILoggingServiceConfigurer
    {
        void Configure(ILoggingServiceProvider loggingServiceProvider);
    }
}