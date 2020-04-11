namespace Axle.Logging
{
    public interface ILoggingServiceConfigurer
    {
        void Configure(ILoggingServiceProvider loggingServiceProvider);
    }
}