using System;


namespace Axle.Logging.Log4net
{
    // ReSharper disable once InconsistentNaming
    public sealed class Log4netLoggingService : ILoggingService
    {
        public ILogger CreateLogger(Type targetType)
        {
            if (targetType == null)
            {
                throw new ArgumentNullException(nameof(targetType));
            }
            return new Log4netLogger(targetType);
        }
        public ILogger CreateLogger<T>() => CreateLogger(typeof(T));
    }
}
