using System;
using System.Diagnostics;
using System.IO;

namespace Axle.Logging
{
    internal sealed class DefaultLogger : ILogger
    {
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly object _syncRoot = new object();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _enableConsoleLogs = true;
        #endif

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Type _targetType;

        public DefaultLogger(Type targetType)
        {
            _targetType = targetType ?? throw new ArgumentNullException(nameof(targetType));
        }

        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        private static void LogToConsole(ILogEntry message)
        {
            lock (_syncRoot)
            lock (typeof(Console))
            lock (Console.Out)
            {
                var oldText = Console.ForegroundColor;
                var oldBg = Console.BackgroundColor;
                ConsoleColor primaryColor, secondaryColor, 
                             defaultColor = oldText, defaultBackground = oldBg,
                             primaryBackground = oldBg, secondaryBackground = oldBg;
                TextWriter writer = Console.Out;
                var abbr = "MSG";

                switch (message.Severity)
                {
                    case LogSeverity.Debug:
                        primaryColor = ConsoleColor.Cyan;
                        secondaryColor = ConsoleColor.Blue;
                        primaryBackground = oldBg;
                        secondaryBackground = oldBg;
                        abbr = "debug";
                        break;
                    case LogSeverity.Info:
                        primaryColor = ConsoleColor.Green;
                        secondaryColor = ConsoleColor.Cyan;
                        primaryBackground = oldBg;
                        secondaryBackground = oldBg;
                        abbr = " info";
                        break;
                    case LogSeverity.Warning:
                        primaryColor = ConsoleColor.Yellow;
                        secondaryColor = ConsoleColor.White;
                        primaryBackground = oldBg;
                        secondaryBackground = oldBg;
                        abbr = " warn";
                        break;
                    case LogSeverity.Error:
                        primaryColor = ConsoleColor.Red;
                        secondaryColor = ConsoleColor.White;
                        primaryBackground = oldBg;
                        secondaryBackground = oldBg;
                        abbr = "error";
                        break;
                    case LogSeverity.Fatal:
                        primaryColor = ConsoleColor.Yellow;
                        secondaryColor = ConsoleColor.White;
                        primaryBackground = ConsoleColor.DarkRed;
                        secondaryBackground = ConsoleColor.DarkRed;
                        writer = Console.Error;
                        abbr = "fatal";
                        break;
                    default:
                        primaryColor = ConsoleColor.Cyan;
                        secondaryColor = ConsoleColor.Blue;
                        primaryBackground = oldBg;
                        secondaryBackground = oldBg;
                        break;
                }
                Console.ResetColor();
                try
                {
                    Console.ForegroundColor = secondaryColor;
                    Console.BackgroundColor = secondaryBackground;
                    writer.Write("{0:yyyy-MM-dd HH:mm:ss} ", message.Timestamp);
                    writer.Flush();

                    Console.ForegroundColor = primaryColor;
                    Console.BackgroundColor = primaryBackground;
                    writer.Write("{0} ", abbr);
                    writer.Flush();
                            
                    #if NETSTANDARD1_6_OR_NEWER || NETFRAMEWORK
                    Console.ForegroundColor = secondaryColor;
                    Console.BackgroundColor = secondaryBackground;
                    writer.Write("{0} ", message.ThreadID);
                    writer.Flush();
                    #endif

                    Console.ForegroundColor = primaryColor;
                    Console.BackgroundColor = primaryBackground;
                    writer.Write("{0} ", message.Type.FullName);
                    writer.Flush();

                    Console.ForegroundColor = defaultColor;
                    Console.BackgroundColor = defaultBackground;
                    writer.Write(message.Message);
                    writer.Flush();
                    if (message.Exception != null)
                    {
                        writer.Write("\n{0}:{1}\n{1}", message.Exception.GetType().FullName, message.Exception.Message, message.Exception.StackTrace);
                        writer.Flush();
                    }
                }
                finally
                {
                    writer.WriteLine();
                    writer.Flush();
                    Console.ForegroundColor = oldText;
                    Console.BackgroundColor = oldBg;
                }
            }
        }
        #endif

        private static void LogToDebug(ILogEntry message) => Debug.WriteLine(message);

        #if NETSTANDARD1_6_OR_NEWER || NETFRAMEWORK
        private static void LogToTrace(ILogEntry message) => Trace.WriteLine(message);
        #endif

        void ILogger.Write(ILogEntry entry)
        {
            #if NETSTANDARD1_6_OR_NEWER || NETFRAMEWORK
            var action = entry.Severity == LogSeverity.Debug ? new Action<ILogEntry>(LogToDebug) : LogToTrace;
            #else
            var action = new Action<ILogEntry>(LogToDebug);
            #endif
            #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
            if (_enableConsoleLogs)
            {
                action += LogToConsole;
            }
            #endif
            action(entry);
        }
        
        Type ILogger.TargetType => _targetType;
    }
}