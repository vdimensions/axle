using System;


namespace Axle.Core.Infrastructure.Logging
{
    internal sealed class DefaultLoggingServiceProvider : ILoggingServiceProvider
    {
        public ILogger Create(Type targetType) => new DefaultLogger(targetType);

        public ILogger Create<T>() => new DefaultLogger(typeof(T));
    }
}