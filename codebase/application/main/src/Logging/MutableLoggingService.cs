using System;
using System.Collections.Concurrent;
using System.Threading;

using Axle.Threading;

namespace Axle.Logging
{
    internal sealed class MutableLoggingService : ILoggingService, ILoggingServiceRegistry
    {
        internal sealed class MutableLogger : ILogger
        {
            private readonly IReadWriteLockProvider _lockProvider;
            private readonly MutableLoggingService _loggingService;
            private volatile int _version = -1;
            private volatile ILogger _logger;

            public MutableLogger(MutableLoggingService loggingService, Type targetType, ILogger logger)
            {
                _loggingService = loggingService;
                _lockProvider = new ReadWriteLockProvider();
                _logger = logger;
                TargetType = targetType;
            }

            public void Write(ILogEntry entry)
            {
                var expectedVersion = _loggingService._version;
                using (_lockProvider.ReadLock.CreateHandle())
                {
                    if (_version == expectedVersion)
                    {
                        _logger.Write(entry);
                        return;
                    }
                }
                using (_lockProvider.UpgradeableReadLock.CreateHandle())
                {
                    if (Interlocked.Exchange(ref _version, expectedVersion) != expectedVersion)
                    {
                        using (_lockProvider.WriteLock.CreateHandle())
                        {
                            Interlocked.Exchange(ref _logger, _loggingService.CreateLoggerInternal(TargetType));
                        }
                    }
                    _logger.Write(entry);
                }
            }

            public Type TargetType { get; }
        }
        
        private readonly ConcurrentDictionary<Type, MutableLogger> _loggersByType = new ConcurrentDictionary<Type, MutableLogger>();
        private ILoggingService _loggingService;
        private int _version = 0;
        private readonly ILock _mutationLock = MonitorLock.Create();

        public MutableLoggingService(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        internal ILogger CreateLoggerInternal(Type targetType)
        {
            return _loggingService.CreateLogger(targetType);
        }

        public ILogger CreateLogger(Type targetType)
        {
            return _loggersByType.GetOrAdd(targetType, t => new MutableLogger(this, t, CreateLoggerInternal(t)));
        }
        public ILogger CreateLogger<T>()
        {
            var targetType = typeof(T);
            return CreateLogger(targetType);
        }

        void ILoggingServiceRegistry.RegisterLoggingService(ILoggingService service)
        {
            using (_mutationLock.CreateHandle())
            {
                _loggingService = service;
                _version++;
            }
        }
    }
}