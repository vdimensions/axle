using Axle.Logging;

namespace Axle.Application
{
    internal interface ISetLoggingService
    {
        ILoggingService LoggingService { get; set; }
    }
}