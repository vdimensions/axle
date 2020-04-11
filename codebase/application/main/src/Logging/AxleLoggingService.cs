using System;


namespace Axle.Logging
{
    internal sealed class AxleLoggingService : ILoggingService
    {
        public ILogger CreateLogger(Type targetType) => new DefaultLogger(targetType);

        public ILogger CreateLogger<T>() => new DefaultLogger(typeof(T));
    }
}