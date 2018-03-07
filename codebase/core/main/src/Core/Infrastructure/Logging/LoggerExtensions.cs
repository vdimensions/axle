using System;

using Axle.Verification;


namespace Axle.Core.Infrastructure.Logging
{
    /// <summary>
    /// A static class to act as a mixin to the <see cref="ILogger"/> interface. 
    /// Provides conventient overloads for logging methods based on the default behavior defined by the <see cref="ILogger"/> interface.
    /// </summary>
    public static class LoggerExtensions
    {
        public static void Write(this ILogger logger, LogSeverity severity, Exception exception)
        {
            logger.VerifyArgument(nameof(logger)).IsNotNull().Value.Write(new LogEntry(severity, logger.TargetType, exception));
        }
        public static void Write(this ILogger logger, LogSeverity severity, Exception exception, string message)
        {
            logger.VerifyArgument(nameof(logger)).IsNotNull().Value.Write(new LogEntry(severity, logger.TargetType, message, exception));
        }
        public static void Write(this ILogger logger, LogSeverity severity, string message)
        {
            logger.VerifyArgument(nameof(logger)).IsNotNull().Value.Write(new LogEntry(severity, logger.TargetType, message));
        }
        public static void Write(this ILogger logger, LogSeverity severity, string format, params object[] args)
        {
            logger.VerifyArgument(nameof(logger)).IsNotNull().Value.Write(new LogEntry(severity, logger.TargetType, string.Format(format, args)));
        }
        public static void Write(this ILogger logger, LogSeverity severity, IFormatProvider formatProvider, string format, params object[] args)
        {
            logger.VerifyArgument(nameof(logger)).IsNotNull().Value.Write(new LogEntry(severity, logger.TargetType, string.Format(formatProvider, format, args)));
        }

        /// <summary>
        /// Writes the provided <paramref name="exception"/> exception as a <see cref="LogSeverity.Debug">debug</see> message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger">logger</see> instance used to log the message.</param>
        /// <param name="exception">The <see cref="Exception"/> to be written to the log.</param>
        /// <returns>
        /// A reference to the <see cref="ILogger">logger</see> instance that wrote the message.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" /> or <paramref name="exception"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Debug"/>
        /// <seealso cref="Exception"/>
        public static void Debug(this ILogger logger, Exception exception) 
        {
            Write(logger, LogSeverity.Debug, exception); 
        }
        public static void Debug(this ILogger logger, Exception exception, string message) 
        {
            Write(logger, LogSeverity.Debug, message, exception); 
        }
        public static void Debug(this ILogger logger, string message)
        {
            Write(logger, LogSeverity.Debug, message);
        }
        public static void Debug(this ILogger logger, string format, params object[] args)
        {
            Write(logger, LogSeverity.Debug, format, args);
        }
        public static void Debug(this ILogger logger, IFormatProvider formatProvider, string format, params object[] args)
        {
            Write(logger, LogSeverity.Debug, formatProvider, format, args);
        }

        /// <summary>
        /// Writes the provided <paramref name="exception"/> exception as a <see cref="LogSeverity.Trace">trace</see> message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger">logger</see> instance used to log the message.</param>
        /// <param name="exception">The <see cref="Exception"/> to be written to the log.</param>
        /// <returns>
        /// A reference to the <see cref="ILogger">logger</see> instance that wrote the message.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" /> or <paramref name="exception"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Trace"/>
        /// <seealso cref="Exception"/>
        public static void Trace(this ILogger logger, Exception exception)
        {
            Write(logger, LogSeverity.Trace, exception);
        }
        public static void Trace(this ILogger logger, Exception exception, string message)
        {
            Write(logger, LogSeverity.Trace, message, exception);
        }
        public static void Trace(this ILogger logger, string message)
        {
            Write(logger, LogSeverity.Trace, message);
        }
        public static void Trace(this ILogger logger, string format, params object[] args)
        {
            Write(logger, LogSeverity.Trace, format, args);
        }
        public static void Trace(this ILogger logger, IFormatProvider formatProvider, string format, params object[] args)
        {
            Write(logger, LogSeverity.Trace, formatProvider, format, args);
        }

        /// <summary>
        /// Writes the provided <paramref name="exception"/> exception as a <see cref="LogSeverity.Warning">warning</see> message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger">logger</see> instance used to log the message.</param>
        /// <param name="exception">The <see cref="Exception"/> to be written to the log.</param>
        /// <returns>
        /// A reference to the <see cref="ILogger">logger</see> instance that wrote the message.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" /> or <paramref name="exception"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Warning"/>
        /// <seealso cref="Exception"/>
        public static void Warn(this ILogger logger, Exception exception)
        {
            Write(logger, LogSeverity.Warning, exception);
        }
        public static void Warn(this ILogger logger, Exception exception, string message)
        {
            Write(logger, LogSeverity.Warning, message, exception);
        }
        public static void Warn(this ILogger logger, string message)
        {
            Write(logger, LogSeverity.Warning, message);
        }
        public static void Warn(this ILogger logger, string format, params object[] args)
        {
            Write(logger, LogSeverity.Warning, format, args);
        }
        public static void Warn(this ILogger logger, IFormatProvider formatProvider, string format, params object[] args)
        {
            Write(logger, LogSeverity.Warning, formatProvider, format, args);
        }

        /// <summary>
        /// Writes the provided <paramref name="exception"/> exception as a <see cref="LogSeverity.Error">error</see> message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger">logger</see> instance used to log the message.</param>
        /// <param name="exception">The <see cref="Exception"/> to be written to the log.</param>
        /// <returns>
        /// A reference to the <see cref="ILogger">logger</see> instance that wrote the message.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" /> or <paramref name="exception"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Error"/>
        /// <seealso cref="Exception"/>
        public static void Error(this ILogger logger, Exception exception)
        {
            Write(logger, LogSeverity.Error, exception);
        }
        public static void Error(this ILogger logger, Exception exception, string message)
        {
            Write(logger, LogSeverity.Error, message, exception);
        }
        public static void Error(this ILogger logger, string message)
        {
            Write(logger, LogSeverity.Error, message);
        }
        public static void Error(this ILogger logger, string format, params object[] args)
        {
            Write(logger, LogSeverity.Error, format, args);
        }
        public static void Error(this ILogger logger, IFormatProvider formatProvider, string format, params object[] args)
        {
            Write(logger, LogSeverity.Error, formatProvider, format, args);
        }

        /// <summary>
        /// Writes the provided <paramref name="exception"/> exception as a <see cref="LogSeverity.Failure">failure</see> message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger">logger</see> instance used to log the message.</param>
        /// <param name="exception">The <see cref="Exception"/> to be written to the log.</param>
        /// <returns>
        /// A reference to the <see cref="ILogger">logger</see> instance that wrote the message.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" /> or <paramref name="exception"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Failure"/>
        /// <seealso cref="Exception"/>
        public static void Fail(this ILogger logger, Exception exception)
        {
            Write(logger, LogSeverity.Failure, exception);
        }
        public static void Fail(this ILogger logger, Exception exception, string message)
        {
            Write(logger, LogSeverity.Failure, message, exception);
        }
        public static void Fail(this ILogger logger, string message)
        {
            Write(logger, LogSeverity.Failure, message);
        }
        public static void Fail(this ILogger logger, string format, params object[] args)
        {
            Write(logger, LogSeverity.Failure, format, args);
        }
        public static void Fail(this ILogger logger, IFormatProvider formatProvider, string format, params object[] args)
        {
            Write(logger, LogSeverity.Failure, formatProvider, format, args);
        }
    }
}