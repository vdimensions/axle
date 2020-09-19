using Axle.Logging;

namespace Axle
{
    internal interface ISetLoggingService
    {
        ILoggingService LoggingService { get; set; }
    }
}