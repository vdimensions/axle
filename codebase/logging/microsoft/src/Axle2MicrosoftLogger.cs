using Microsoft.Extensions.Logging;

using System;

namespace Axle.Logging.Microsoft
{
    using IMSLogger = global::Microsoft.Extensions.Logging.ILogger;

    public sealed class Axle2MicrosoftLogger : IMSLogger
    {
        private readonly ILogger _logger;

        public Axle2MicrosoftLogger(ILogger logger)
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
}
