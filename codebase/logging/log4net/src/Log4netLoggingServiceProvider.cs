using System;

using Axle.Core.Infrastructure.Logging;


namespace Axle.Logging.Log4net
{
    // ReSharper disable once InconsistentNaming
    public sealed class Log4netLoggingServiceProvider : ILoggingServiceProvider
    {
        public ILogger Create(Type targetType)
        {
            if (targetType == null)
            {
                throw new ArgumentNullException(nameof(targetType));
            }
            return new Log4netLogger(targetType);
        }
        public ILogger Create<T>() => Create(typeof(T));
    }
}
