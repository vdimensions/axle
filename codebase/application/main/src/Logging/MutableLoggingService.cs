using System;
using System.Collections.Concurrent;
#if !UNITY_WEBGL
using System.Threading;
using Axle.Threading;
#endif

namespace Axle.Logging
{
    internal sealed class MutableLoggingService : ILoggingService, ILoggingServiceRegistry
    {
        internal sealed class MutableLogger : ILogger
        {
            #if !UNITY_WEBGL
            private readonly IReadWriteLockProvider _lockProvider;
            #endif
            private readonly MutableLoggingService _loggingService;
            private volatile int _version = -1;
            private volatile ILogger _logger;

            public MutableLogger(MutableLoggingService loggingService, Type targetType, ILogger logger)
            {
                _loggingService = loggingService;
                #if !UNITY_WEBGL
                _lockProvider = new ReadWriteLockProvider();
                #endif
                _logger = logger;
                TargetType = targetType;
            }

            public void Write(ILogEntry entry)
            {
                #if !UNITY_WEBGL
                var expectedVersion = _loggingService._version;
                using (_lockProvider.ReadLock.CreateHandle())
                {
                    if (_version == expectedVersion)
                    {
                        _logger.Write(entry);
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
                #else
                _logger.Write(entry);
                #endif
            }

            public Type TargetType { get; }
        }
        
        private readonly ConcurrentDictionary<Type, MutableLogger> _loggersByType = new ConcurrentDictionary<Type, MutableLogger>();
        private ILoggingService _loggingService;
        private int _version = 0;
        #if !UNITY_WEBGL
        private readonly ILock _mutationLock = MonitorLock.Create();
        #endif

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
            #if !UNITY_WEBGL
            using (_mutationLock.CreateHandle())
            #endif
            {
                _loggingService = service;
                _version++;
            }
        }
    }
}