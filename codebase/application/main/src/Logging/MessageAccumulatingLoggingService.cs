using System;
using System.Collections.Concurrent;

namespace Axle.Logging
{
    [Obsolete]
    internal sealed class MessageAccumulatingLoggingService : ILoggingService, IDisposable
    {
        internal sealed class MessageAccumulatingLogger : ILogger
        {
            private readonly MessageAccumulatingLoggingService _loggingService;

            public MessageAccumulatingLogger(MessageAccumulatingLoggingService loggingService, Type targetType)
            {
                _loggingService = loggingService;
                TargetType = targetType;
            }

            public void Write(ILogEntry entry)
            {
                _loggingService._entries.Enqueue(entry);
            }

            public Type TargetType { get; }
        }
        
        private readonly ConcurrentQueue<ILogEntry> _entries = new ConcurrentQueue<ILogEntry>();

        public ILogger CreateLogger(Type targetType) => new MessageAccumulatingLogger(this, targetType);
        public ILogger CreateLogger<T>() => new MessageAccumulatingLogger(this, typeof(T));

        public void FlushTo(ILoggingService targetLoggingService)
        {
            foreach (var entry in _entries.ToArray())
            {
                targetLoggingService.CreateLogger(entry.Type).Write(entry);
            }
        }
        
        public void Dispose()
        {
            ILoggingService targetLoggingService = new AxleLoggingService();
            while (_entries.TryDequeue(out var entry))
            {
                targetLoggingService.CreateLogger(entry.Type).Write(entry);
            }
        }

        void IDisposable.Dispose() => Dispose();

    }
}