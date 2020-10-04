using System;
using Microsoft.Extensions.Logging;

namespace Axle.Web.AspNetCore.Logging
{
    internal sealed class MicrosoftLogger : Axle.Logging.ILogger
    {
        private readonly ILogger _logger;

        public MicrosoftLogger(Type targetType, ILogger logger)
        {
            TargetType = targetType;
            _logger = logger;
        }

        private void LogMessage(Axle.Logging.ILogEntry entry)
        {
            switch (entry.Severity)
            {
                case Axle.Logging.LogSeverity.Debug:
                    _logger.LogDebug(entry.Message);
                    break;
                case Axle.Logging.LogSeverity.Info:
                    _logger.LogInformation(entry.Message);
                    break;
                case Axle.Logging.LogSeverity.Warning:
                    _logger.LogWarning(entry.Message);
                    break;
                case Axle.Logging.LogSeverity.Error:
                    _logger.LogError(entry.Message);
                    break;
                case Axle.Logging.LogSeverity.Fatal:
                    _logger.LogCritical(entry.Message);
                    break;
            }
        }

        private void LogException(Axle.Logging.ILogEntry entry, Exception ex)
        {
            switch (entry.Severity)
            {
                case Axle.Logging.LogSeverity.Debug:
                    _logger.LogDebug(ex, entry.Message);
                    break;
                case Axle.Logging.LogSeverity.Info:
                    _logger.LogInformation(ex, entry.Message);
                    break;
                case Axle.Logging.LogSeverity.Warning:
                    _logger.LogWarning(ex, entry.Message);
                    break;
                case Axle.Logging.LogSeverity.Error:
                    _logger.LogError(ex, entry.Message);
                    break;
                case Axle.Logging.LogSeverity.Fatal:
                    _logger.LogCritical(ex, entry.Message);
                    break;
            }
        }

        public void Write(Axle.Logging.ILogEntry entry)
        {
            if (entry.Exception != null)
            {
                LogException(entry, entry.Exception);
            }
            else
            {
                LogMessage(entry);
            }
        }

        public Type TargetType { get; }
    }
}


