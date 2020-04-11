namespace Axle.Logging
{
    public interface ILoggingServiceProvider
    {
        void AddLoggingService(ILoggingService service);
    }
}