using Axle.Logging;

namespace Axle.Application
{
    [System.Obsolete]
    internal interface ISetLoggingService
    {
        ILoggingService LoggingService { get; set; }
    }
}