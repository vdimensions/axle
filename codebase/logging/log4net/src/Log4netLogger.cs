using System;
using System.Diagnostics;

using Axle.Core.Infrastructure.Logging;
using Axle.Verification;

using log4net;


namespace Axle.Logging.Log4net
{
    // ReSharper disable once InconsistentNaming
    internal sealed class Log4netLogger : Axle.Core.Infrastructure.Logging.ILogger
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Type _targetType;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILog _log;

        public Log4netLogger(Type targetType)
        {
            _targetType = targetType.VerifyArgument(nameof(targetType)).IsNotNull().Value;
            _log = LogManager.GetLogger(targetType);
        }

        #region Implementation of ILogger
        void Axle.Core.Infrastructure.Logging.ILogger.Write(ILogEntry entry)
        {
            switch (entry.Severity)
            {
                case LogSeverity.Fatal:
                    _log.Fatal(entry.Message, entry.Exception);
                    break;
                case LogSeverity.Error:
                    _log.Error(entry.Message, entry.Exception);
                    break;
                case LogSeverity.Warning:
                    _log.Warn(entry.Message, entry.Exception);
                    break;
                case LogSeverity.Debug:
                    _log.Debug(entry.Message, entry.Exception);
                    break;
                //case LogSeverity.Info:
                default:
                    _log.Info(entry.Message, entry.Exception);
                    break;
            }
        }
        
        Type Axle.Core.Infrastructure.Logging.ILogger.TargetType => _targetType;
        #endregion
    }
}