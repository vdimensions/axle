using System;


namespace Axle.Logging.Log4net
{
    // ReSharper disable once InconsistentNaming
    public sealed class Log4netLoggingServiceProvider : ILoggingServiceProvider
    {
        [Obsolete]
        ILogger ILoggingServiceProvider.Create(Type targetType) => CreateLogger(targetType);
        [Obsolete]
        ILogger ILoggingServiceProvider.Create<T>() => CreateLogger<T>();

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
