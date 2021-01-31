using System;


namespace Axle.Logging
{
    internal sealed class AxleLoggingService : ILoggingService
    {
        public ILogger CreateLogger(Type targetType) => new AxleLogger(targetType);

        public ILogger CreateLogger<T>() => new AxleLogger(typeof(T));
    }
}