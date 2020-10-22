using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Verification;

namespace Axle.Logging
{
    /// <summary>
    /// A static class that provides various overloads of the logging API as defined by the <see cref="ILogger"/>
    /// interface.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class LoggerExtensions
    {
        /// <summary>
        /// Writes the provided <paramref name="exception"/> with the specified <paramref name="severity"/> into
        /// a log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="severity">
        /// The <see cref="LogSeverity"/> use for the log entry.
        /// </param>
        /// <param name="exception">
        /// The <see cref="Exception"/> to be written to the log.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" /> or <paramref name="exception"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="Exception"/>
        public static void Write(this ILogger logger, LogSeverity severity, Exception exception)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(logger, nameof(logger)));
            logger.Write(new LogEntry(severity, logger.TargetType, exception));
        }
        /// <summary>
        /// Writes the provided <paramref name="exception"/> and accompanying <paramref name="message"/>
        /// with the specified <paramref name="severity"/> into a log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="severity">
        /// The <see cref="LogSeverity"/> use for the log entry.
        /// </param>
        /// <param name="exception">
        /// The <see cref="Exception"/> to be written to the log.
        /// </param>
        /// <param name="message">
        /// A custom message to be logged as part of the log entry.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" />, <paramref name="exception"/> or <paramref name="message"/> is
        /// <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="Exception"/>
        public static void Write(this ILogger logger, LogSeverity severity, Exception exception, string message)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(logger, nameof(logger)));
            logger.Write(new LogEntry(severity, logger.TargetType, message, exception));
        }
        /// <summary>
        /// Writes the provided <paramref name="message"/> with the specified <paramref name="severity"/> into
        /// a log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="severity">
        /// The <see cref="LogSeverity"/> use for the log entry.
        /// </param>
        /// <param name="message">
        /// The message to be logged as part of the log entry.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" /> or <paramref name="message"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="Exception"/>
        public static void Write(this ILogger logger, LogSeverity severity, string message)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(logger, nameof(logger)));
            Verifier.IsNotNull(Verifier.VerifyArgument(message, nameof(message)));
            logger.Write(new LogEntry(severity, logger.TargetType, message));
        }
        /// <summary>
        /// Writes a message using the provided <paramref name="format"/> string and <paramref name="args"/>
        /// with the specified <paramref name="severity"/> into a log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="severity">
        /// The <see cref="LogSeverity"/> use for the log entry.
        /// </param>
        /// <param name="format">
        /// The format string that is used to format the message.
        /// </param>
        /// <param name="args">
        /// The collection of values which will be formatted into the logged message using the specified
        /// <paramref name="format"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" /> or <paramref name="format"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="Exception"/>
        public static void Write(this ILogger logger, LogSeverity severity, string format, params object[] args)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(logger, nameof(logger)));
            Verifier.IsNotNull(Verifier.VerifyArgument(format, nameof(format)));
            logger.Write(new LogEntry(severity, logger.TargetType, string.Format(format, args)));
        }
        /// <summary>
        /// Writes a message using the provided <paramref name="format"/> string and <paramref name="args"/>
        /// with the specified <paramref name="severity"/> into a log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="severity">
        /// The <see cref="LogSeverity"/> use for the log entry.
        /// </param>
        /// <param name="formatProvider">
        /// An <see cref="IFormatProvider"/> implementation that supplies culture-specific formatting information.
        /// </param>
        /// <param name="format">
        /// The format string that is used to format the message.
        /// </param>
        /// <param name="args">
        /// The collection of values which will be formatted into the logged message using the specified
        /// <paramref name="format"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" /> or <paramref name="format"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="Exception"/>
        public static void Write(this ILogger logger, LogSeverity severity, IFormatProvider formatProvider, string format, params object[] args)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(logger, nameof(logger)));
            logger.Write(new LogEntry(severity, logger.TargetType, string.Format(formatProvider, format, args)));
        }

        /// <summary>
        /// Writes the provided <paramref name="exception"/> into
        /// a <see cref="LogSeverity.Debug">debug</see> log entry.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger">logger</see> instance used to log the message.</param>
        /// <param name="exception">The <see cref="Exception"/> to be written to the log.</param>
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
        /// <summary>
        /// Writes the provided <paramref name="exception"/> and accompanying <paramref name="message"/> into
        /// a <see cref="LogSeverity.Debug">debug</see> log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="exception">
        /// The <see cref="Exception"/> to be written to the log.
        /// </param>
        /// <param name="message">
        /// A custom message to be logged as part of the log entry.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" />, <paramref name="exception"/> or <paramref name="message"/> is
        /// <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Debug"/>
        /// <seealso cref="Exception"/>
        public static void Debug(this ILogger logger, Exception exception, string message) 
        {
            Write(logger, LogSeverity.Debug, message, exception); 
        }
        /// <summary>
        /// Writes the provided <paramref name="message"/> into
        /// a <see cref="LogSeverity.Debug">debug</see> log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="message">
        /// The message to be logged as part of the log entry.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" /> or <paramref name="message"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Debug"/>
        /// <seealso cref="Exception"/>
        public static void Debug(this ILogger logger, string message)
        {
            Write(logger, LogSeverity.Debug, message);
        }
        /// <summary>
        /// Writes a message using the provided <paramref name="format"/> string and <paramref name="args"/> into
        /// a <see cref="LogSeverity.Debug">debug</see> log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="format">
        /// The format string that is used to format the message.
        /// </param>
        /// <param name="args">
        /// The collection of values which will be formatted into the logged message using the specified
        /// <paramref name="format"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" /> or <paramref name="format"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Debug"/>
        /// <seealso cref="Exception"/>
        public static void Debug(this ILogger logger, string format, params object[] args)
        {
            Write(logger, LogSeverity.Debug, format, args);
        }
        /// <summary>
        /// Writes a message using the provided <paramref name="format"/> string and <paramref name="args"/> into
        /// a <see cref="LogSeverity.Debug">debug</see> log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="formatProvider">
        /// An <see cref="IFormatProvider"/> implementation that supplies culture-specific formatting information.
        /// </param>
        /// <param name="format">
        /// The format string that is used to format the message.
        /// </param>
        /// <param name="args">
        /// The collection of values which will be formatted into the logged message using the specified
        /// <paramref name="format"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" /> or <paramref name="format"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Debug"/>
        /// <seealso cref="Exception"/>
        public static void Debug(this ILogger logger, IFormatProvider formatProvider, string format, params object[] args)
        {
            Write(logger, LogSeverity.Debug, formatProvider, format, args);
        }

        /// <summary>
        /// Writes the provided <paramref name="exception"/> into
        /// an <see cref="LogSeverity.Info">informational</see> log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="exception">
        /// The <see cref="Exception"/> to be written to the log.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" /> or <paramref name="exception"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Debug"/>
        /// <seealso cref="Exception"/>
        public static void Info(this ILogger logger, Exception exception)
        {
            Write(logger, LogSeverity.Info, exception);
        }
        /// <summary>
        /// Writes the provided <paramref name="exception"/> and accompanying <paramref name="message"/> into
        /// a <see cref="LogSeverity.Info">informational</see> log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="exception">
        /// The <see cref="Exception"/> to be written to the log.
        /// </param>
        /// <param name="message">
        /// A custom message to be logged as part of the log entry.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" />, <paramref name="exception"/> or <paramref name="message"/> is
        /// <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Info"/>
        /// <seealso cref="Exception"/>
        public static void Info(this ILogger logger, Exception exception, string message)
        {
            Write(logger, LogSeverity.Info, message, exception);
        }
        /// <summary>
        /// Writes the provided <paramref name="message"/> into
        /// a <see cref="LogSeverity.Info">informational</see> log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="message">
        /// The message to be logged as part of the log entry.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" /> or <paramref name="message"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Info"/>
        /// <seealso cref="Exception"/>
        public static void Info(this ILogger logger, string message)
        {
            Write(logger, LogSeverity.Info, message);
        }
        /// <summary>
        /// Writes a message using the provided <paramref name="format"/> string and <paramref name="args"/> into
        /// a <see cref="LogSeverity.Info">informational</see> log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="format">
        /// The format string that is used to format the message.
        /// </param>
        /// <param name="args">
        /// The collection of values which will be formatted into the logged message using the specified
        /// <paramref name="format"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" /> or <paramref name="format"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Info"/>
        /// <seealso cref="Exception"/>
        public static void Info(this ILogger logger, string format, params object[] args)
        {
            Write(logger, LogSeverity.Info, format, args);
        }
        /// <summary>
        /// Writes a message using the provided <paramref name="format"/> string and <paramref name="args"/> into
        /// a <see cref="LogSeverity.Info">informational</see> log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="formatProvider">
        /// An <see cref="IFormatProvider"/> implementation that supplies culture-specific formatting information.
        /// </param>
        /// <param name="format">
        /// The format string that is used to format the message.
        /// </param>
        /// <param name="args">
        /// The collection of values which will be formatted into the logged message using the specified
        /// <paramref name="format"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" /> or <paramref name="format"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Info"/>
        /// <seealso cref="Exception"/>
        public static void Info(this ILogger logger, IFormatProvider formatProvider, string format, params object[] args)
        {
            Write(logger, LogSeverity.Info, formatProvider, format, args);
        }

        /// <summary>
        /// Writes the provided <paramref name="exception"/> into
        /// a <see cref="LogSeverity.Warning">warning</see> log entry.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger">logger</see> instance used to log the message.</param>
        /// <param name="exception">The <see cref="Exception"/> to be written to the log.</param>
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
        /// <summary>
        /// Writes the provided <paramref name="exception"/> and accompanying <paramref name="message"/> into
        /// a <see cref="LogSeverity.Warning">warning</see> log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="exception">
        /// The <see cref="Exception"/> to be written to the log.
        /// </param>
        /// <param name="message">
        /// A custom message to be logged as part of the log entry.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" />, <paramref name="exception"/> or <paramref name="message"/> is
        /// <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Warning"/>
        /// <seealso cref="Exception"/>
        public static void Warn(this ILogger logger, Exception exception, string message)
        {
            Write(logger, LogSeverity.Warning, message, exception);
        }
        /// <summary>
        /// Writes the provided <paramref name="message"/> into
        /// a <see cref="LogSeverity.Warning">warning</see> log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="message">
        /// The message to be logged as part of the log entry.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" /> or <paramref name="message"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Warning"/>
        /// <seealso cref="Exception"/>
        public static void Warn(this ILogger logger, string message)
        {
            Write(logger, LogSeverity.Warning, message);
        }
        /// <summary>
        /// Writes a message using the provided <paramref name="format"/> string and <paramref name="args"/> into
        /// a <see cref="LogSeverity.Warning">warning</see> log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="format">
        /// The format string that is used to format the message.
        /// </param>
        /// <param name="args">
        /// The collection of values which will be formatted into the logged message using the specified
        /// <paramref name="format"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" /> or <paramref name="format"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Warning"/>
        /// <seealso cref="Exception"/>
        public static void Warn(this ILogger logger, string format, params object[] args)
        {
            Write(logger, LogSeverity.Warning, format, args);
        }
        /// <summary>
        /// Writes a message using the provided <paramref name="format"/> string and <paramref name="args"/> into
        /// a <see cref="LogSeverity.Warning">warning</see> log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="formatProvider">
        /// An <see cref="IFormatProvider"/> implementation that supplies culture-specific formatting information.
        /// </param>
        /// <param name="format">
        /// The format string that is used to format the message.
        /// </param>
        /// <param name="args">
        /// The collection of values which will be formatted into the logged message using the specified
        /// <paramref name="format"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" /> or <paramref name="format"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Warning"/>
        /// <seealso cref="Exception"/>
        public static void Warn(this ILogger logger, IFormatProvider formatProvider, string format, params object[] args)
        {
            Write(logger, LogSeverity.Warning, formatProvider, format, args);
        }

        /// <summary>
        /// Writes the provided <paramref name="exception"/> into
        /// a <see cref="LogSeverity.Error">error</see> log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="exception">
        /// The <see cref="Exception"/> to be written to the log.
        /// </param>
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
        /// <summary>
        /// Writes the provided <paramref name="exception"/> and accompanying <paramref name="message"/> into
        /// a <see cref="LogSeverity.Error">error</see> log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="exception">
        /// The <see cref="Exception"/> to be written to the log.
        /// </param>
        /// <param name="message">
        /// A custom message to be logged as part of the log entry.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" />, <paramref name="exception"/> or <paramref name="message"/> is
        /// <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Error"/>
        /// <seealso cref="Exception"/>
        public static void Error(this ILogger logger, Exception exception, string message)
        {
            Write(logger, LogSeverity.Error, message, exception);
        }
        /// <summary>
        /// Writes the provided <paramref name="message"/> into
        /// a <see cref="LogSeverity.Error">error</see> log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="message">
        /// The message to be logged as part of the log entry.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" /> or <paramref name="message"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Error"/>
        /// <seealso cref="Exception"/>
        public static void Error(this ILogger logger, string message)
        {
            Write(logger, LogSeverity.Error, message);
        }
        /// <summary>
        /// Writes a message using the provided <paramref name="format"/> string and <paramref name="args"/> into
        /// a <see cref="LogSeverity.Error">error</see> log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="format">
        /// The format string that is used to format the message.
        /// </param>
        /// <param name="args">
        /// The collection of values which will be formatted into the logged message using the specified
        /// <paramref name="format"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" /> or <paramref name="format"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Error"/>
        /// <seealso cref="Exception"/>
        public static void Error(this ILogger logger, string format, params object[] args)
        {
            Write(logger, LogSeverity.Error, format, args);
        }
        /// <summary>
        /// Writes a message using the provided <paramref name="format"/> string and <paramref name="args"/> into
        /// a <see cref="LogSeverity.Error">error</see> log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="formatProvider">
        /// An <see cref="IFormatProvider"/> implementation that supplies culture-specific formatting information.
        /// </param>
        /// <param name="format">
        /// The format string that is used to format the message.
        /// </param>
        /// <param name="args">
        /// The collection of values which will be formatted into the logged message using the specified
        /// <paramref name="format"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" /> or <paramref name="format"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Error"/>
        /// <seealso cref="Exception"/>
        public static void Error(this ILogger logger, IFormatProvider formatProvider, string format, params object[] args)
        {
            Write(logger, LogSeverity.Error, formatProvider, format, args);
        }

        /// <summary>
        /// Writes the provided <paramref name="exception"/> exception into
        /// a <see cref="LogSeverity.Fatal">fatal error</see> log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="exception">
        /// The <see cref="Exception"/> to be written to the log.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" /> or <paramref name="exception"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Fatal"/>
        /// <seealso cref="Exception"/>
        public static void Fatal(this ILogger logger, Exception exception)
        {
            Write(logger, LogSeverity.Fatal, exception);
        }
        /// <summary>
        /// Writes the provided <paramref name="exception"/> and accompanying <paramref name="message"/> into
        /// a <see cref="LogSeverity.Fatal">fatal error</see> log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="exception">
        /// The <see cref="Exception"/> to be written to the log.
        /// </param>
        /// <param name="message">
        /// A custom message to be logged as part of the log entry.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" />, <paramref name="exception"/> or <paramref name="message"/> is
        /// <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Fatal"/>
        /// <seealso cref="Exception"/>
        public static void Fatal(this ILogger logger, Exception exception, string message)
        {
            Write(logger, LogSeverity.Fatal, message, exception);
        }
        /// <summary>
        /// Writes the provided <paramref name="message"/> into
        /// a <see cref="LogSeverity.Fatal">fatal error</see> log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="message">
        /// The message to be logged as part of the log entry.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" /> or <paramref name="message"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Fatal"/>
        /// <seealso cref="Exception"/>
        public static void Fatal(this ILogger logger, string message)
        {
            Write(logger, LogSeverity.Fatal, message);
        }
        /// <summary>
        /// Writes a message using the provided <paramref name="format"/> string and <paramref name="args"/> into
        /// a <see cref="LogSeverity.Fatal">fatal error</see> log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="format">
        /// The format string that is used to format the message.
        /// </param>
        /// <param name="args">
        /// The collection of values which will be formatted into the logged message using the specified
        /// <paramref name="format"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" /> or <paramref name="format"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Fatal"/>
        /// <seealso cref="Exception"/>
        public static void Fatal(this ILogger logger, string format, params object[] args)
        {
            Write(logger, LogSeverity.Fatal, format, args);
        }
        /// <summary>
        /// Writes a message using the provided <paramref name="format"/> string and <paramref name="args"/> into
        /// a <see cref="LogSeverity.Fatal">fatal error</see> log entry.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger">logger</see> instance used to log the message.
        /// </param>
        /// <param name="formatProvider">
        /// An <see cref="IFormatProvider"/> implementation that supplies culture-specific formatting information.
        /// </param>
        /// <param name="format">
        /// The format string that is used to format the message.
        /// </param>
        /// <param name="args">
        /// The collection of values which will be formatted into the logged message using the specified
        /// <paramref name="format"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="logger" /> or <paramref name="format"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="LogSeverity"/>
        /// <seealso cref="LogSeverity.Fatal"/>
        /// <seealso cref="Exception"/>
        public static void Fatal(this ILogger logger, IFormatProvider formatProvider, string format, params object[] args)
        {
            Write(logger, LogSeverity.Fatal, formatProvider, format, args);
        }
    }
}