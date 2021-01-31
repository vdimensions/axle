using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Axle.Threading;

namespace Axle.Logging
{
    [Obsolete]
    internal sealed class AggregatingLoggingService : ILoggingService, IDisposable, ILoggingServiceRegistry
    {
        internal sealed class AggregatingLogger : ILogger
        {
            private readonly IReadWriteLockProvider _lockProvider;
            private readonly AggregatingLoggingService _aggregatingLoggingService;
            private volatile int _version;
            private volatile IList<ILogger> _loggers = new List<ILogger>();

            public AggregatingLogger(AggregatingLoggingService aggregatingLoggingService, Type targetType)
            {
                _aggregatingLoggingService = aggregatingLoggingService;
                _lockProvider = new ReadWriteLockProvider();
                _version = -1;  // always at -1, to be different than the value of `_aggregatingLoggingService._version`
                TargetType = targetType;
            }

            public void Write(ILogEntry entry)
            {
                var expectedVersion = _aggregatingLoggingService._version;
                using (_lockProvider.ReadLock.CreateHandle())
                {
                    if (_version == expectedVersion)
                    {
                        foreach (var logger in _loggers)
                        {
                            logger.Write(entry);
                        }
                        return;
                    }
                }
                using (_lockProvider.UpgradeableReadLock.CreateHandle())
                {
                    if (Interlocked.Exchange(ref _version, expectedVersion) != expectedVersion)
                    {
                        using (_lockProvider.WriteLock.CreateHandle())
                        {
                            var accumulatingLoggingService = _aggregatingLoggingService._accumulatingLoggingService;
                            if (accumulatingLoggingService != null)
                            {
                                Interlocked.Exchange(ref _loggers, new[] {accumulatingLoggingService.CreateLogger(TargetType)});
                            }
                            else
                            {
                                Interlocked.Exchange(ref _loggers, _aggregatingLoggingService._loggingServices.Select(x => x.CreateLogger(TargetType)).ToList());
                            }
                        }
                    }
                    foreach (var logger in _loggers)
                    {
                        logger.Write(entry);
                    }
                }
            }

            public Type TargetType { get; }
        }
        
        private readonly ConcurrentDictionary<Type, ILogger> _loggersByType = new ConcurrentDictionary<Type, ILogger>();
        private readonly ConcurrentQueue<ILoggingService> _loggingServices;
        private volatile MessageAccumulatingLoggingService _accumulatingLoggingService = new MessageAccumulatingLoggingService();
        private volatile int _version = 0;

        public AggregatingLoggingService(params ILoggingService[] loggingServices)
        {
            _loggingServices = new ConcurrentQueue<ILoggingService>(loggingServices.Where(x => x != null));
        }

        public ILogger CreateLogger(Type targetType)
        {
            return _loggersByType.GetOrAdd(targetType, t => new AggregatingLogger(this, t));

        }
        public ILogger CreateLogger<T>()
        {
            var targetType = typeof(T);
            return CreateLogger(targetType);
        }

        void IDisposable.Dispose()
        {
            while (_loggingServices.TryDequeue(out var l)) { }
            _loggersByType.Clear();
            _accumulatingLoggingService?.Dispose();
        }

        void ILoggingServiceRegistry.RegisterLoggingService(ILoggingService service)
        {
            _loggingServices.Enqueue(service);
            Interlocked.Increment(ref _version);
        }

        public void FlushMessages()
        {
            var acc = _accumulatingLoggingService;
            if (_loggingServices.IsEmpty)
            {
                _loggingServices.Enqueue(new AxleLoggingService());
                Interlocked.Increment(ref _version);
            }
            Interlocked.Exchange(ref _accumulatingLoggingService, null);
            Interlocked.Increment(ref _version);
            acc.FlushTo(this);
        }
    }
}