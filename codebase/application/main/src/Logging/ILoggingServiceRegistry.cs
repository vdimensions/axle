namespace Axle.Logging
{
    public interface ILoggingServiceRegistry
    {
        void RegisterLoggingService(ILoggingService service);
    }
}