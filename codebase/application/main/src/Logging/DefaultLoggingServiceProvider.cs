using System;


namespace Axle.Logging
{
    internal sealed class DefaultLoggingServiceProvider : ILoggingServiceProvider
    {
        [Obsolete]
        ILogger ILoggingServiceProvider.Create(Type targetType) => CreateLogger(targetType);
        
        [Obsolete]
        ILogger ILoggingServiceProvider.Create<T>() => CreateLogger<T>();

        public ILogger CreateLogger(Type targetType) => new DefaultLogger(targetType);

        public ILogger CreateLogger<T>() => new DefaultLogger(typeof(T));
    }
}