using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Axle.Logging.Microsoft
{
    public sealed class Axle2MicrosoftLogger : global::Microsoft.Extensions.Logging.ILogger
    {
        private readonly Axle.Logging.ILogger _logger;

        public Axle2MicrosoftLogger(Axle.Logging.ILogger logger)
        {
            this._logger = logger;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            // TODO
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            LogSeverity severity = LogSeverity.None;
            switch (logLevel)
            {
                case LogLevel.Trace:
                case LogLevel.Debug:
                    severity = LogSeverity.Debug;
                    break;
                case LogLevel.Information:
                    severity = LogSeverity.Info;
                    break;
                case LogLevel.Warning:
                    severity = LogSeverity.Warning;
                    break;
                case LogLevel.Error:
                    severity = LogSeverity.Error;
                    break;
                case LogLevel.Critical:
                    severity = LogSeverity.Fatal;
                    break;
            }

        }
    }
    public sealed class Axle2MicrosoftLoggerProvider : global::Microsoft.Extensions.Logging.ILoggerProvider
    {
        public global::Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
    public sealed class Axle2MicrosoftLoggerFactory : global::Microsoft.Extensions.Logging.ILoggerFactory
    {
        public void AddProvider(ILoggerProvider provider)
        {
            throw new NotImplementedException();
        }

        public global::Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
