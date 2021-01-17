using System;
using Axle.Logging;
using UnityEngine;

namespace Axle.Application.Unity.Logging
{
    using ILogger = Axle.Logging.ILogger;

    public sealed class UnityLoggingServiceProvider : ILoggingService
    {
        private class ContextfulLogger : ILogger
        {
            public ContextfulLogger(GameObject o, Type targetType)
            {
                Context = o;
                TargetType = targetType;
            }

            public void Write(ILogEntry entry)
            {
                switch (entry.Severity)
                {
                    case LogSeverity.Fatal:
                    case LogSeverity.Error:
                        Debug.LogError(entry.Message, Context);
                        break;
                    case LogSeverity.Warning:
                        Debug.LogWarning(entry.Message, Context);
                        break;
                    default:
                        Debug.Log(entry.Message, Context);
                        break;
                }
            }

            public Type TargetType { get; }
            private GameObject Context { get; }
        }

        private class ContextlessLogger : ILogger
        {
            public ContextlessLogger(Type targetType)
            {
                TargetType = targetType;
            }

            public void Write(ILogEntry entry)
            {
                switch (entry.Severity)
                {
                    case LogSeverity.Fatal:
                    case LogSeverity.Error:
                        Debug.LogError(entry.Message);
                        break;
                    case LogSeverity.Warning:
                        Debug.LogWarning(entry.Message);
                        break;
                    default:
                        Debug.Log(entry.Message);
                        break;
                }
            }

            public Type TargetType { get; }
        }

        private readonly GameObject _context;

        public UnityLoggingServiceProvider(GameObject context)
        {
            _context = context;
        }

        public ILogger CreateLogger(Type targetType)
        {
            return _context == null 
                ? new ContextlessLogger(targetType) as ILogger 
                : new ContextfulLogger(_context, targetType);
        }
        public ILogger CreateLogger<T>() => CreateLogger(typeof(T));
        public ILogger CreateLogger<T>(T context)
        {
            return _context == null 
                ? new ContextlessLogger(typeof(T)) as ILogger 
                : new ContextfulLogger(_context, _context.GetType());
        }
    }
}