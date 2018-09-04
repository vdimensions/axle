using System;
using System.Diagnostics;


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
                Console.ResetColor();
                try
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Out.Write("{0:yyyy-MM-dd HH:mm:ss} ", message.Timestamp);
                    Console.Out.Flush();
                    #if NETSTANDARD1_6_OR_NEWER || NETFRAMEWORK
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Out.Write(message.ThreadID);
                    Console.Out.Flush();
                    #endif
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Out.Write(" [");
                    Console.Out.Flush();
                    switch (message.Severity)
                    {
                        case LogSeverity.Debug:
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            break;
                        case LogSeverity.Trace:
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case LogSeverity.Warning:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        case LogSeverity.Error:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            break;
                        case LogSeverity.Fatal:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                    Console.Out.Write(message.Severity);
                    Console.Out.Flush();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Out.Write("] ");
                    Console.Out.Flush();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Out.Write(message.Type.FullName);
                    Console.Out.Flush();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Out.Write(": ");
                    Console.Out.Write(message.Message);
                    Console.Out.Flush();
                    if (message.Exception != null)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Out.WriteLine();
                        Console.Out.Write(message.Exception.StackTrace);
                        Console.Out.Flush();
                    }
                }
                finally
                {
                    Console.Out.WriteLine();
                    Console.Out.Flush();
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