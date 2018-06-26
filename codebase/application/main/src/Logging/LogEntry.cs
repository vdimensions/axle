using System;
using System.Diagnostics;

#if NETSTANDARD1_6_OR_NEWER || !NETSTANDARD
using System.Threading;
#endif


namespace Axle.Application.Logging
{
    /// <summary>
    /// The default <see cref="ILogEntry"/> implementation provided by the Axle Framework.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    [Serializable]
    #endif
    public sealed class LogEntry : ILogEntry
    {
        #if !NETSTANDARD || NETSTANDARD1_6_OR_NEWER
        private static string ThreadName(Thread t) => t.Name ?? $"Thread-{t.ManagedThreadId}";
        #endif

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DateTime _timestamp;
        #if !NETSTANDARD || NETSTANDARD1_6_OR_NEWER
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string _threadID;
        #endif
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly LogSeverity _severity;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Type _type;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string _message;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Exception _exception;

        #if !NETSTANDARD || NETSTANDARD1_6_OR_NEWER
        public LogEntry(DateTime timestamp, string threadID, LogSeverity severity, Type type, string message) 
            : this(timestamp,threadID, severity, type, message, null) { }
        public LogEntry(DateTime timestamp, string threadID, LogSeverity severity, Type type, Exception exception) 
            : this(timestamp,threadID, severity, type, exception.Message, exception) { }
        public LogEntry(DateTime timestamp, string threadID, LogSeverity severity, Type type, string message, Exception exception)
        {
            _timestamp = timestamp;
            _threadID = threadID;
            _severity = severity;
            _type = type;
            if (exception != null && string.IsNullOrEmpty(message))
            {
                _message = exception.Message;
            }
            else
            {
                _message = message;
            }
            _exception = exception;
        }

        public LogEntry(string threadID, LogSeverity severity, Type type, string message)
            : this(DateTime.Now, threadID, severity, type, message, null) { }
        public LogEntry(string threadID, LogSeverity severity, Type type, string message, Exception exception)
            : this(DateTime.Now, threadID, severity, type, message, exception) { }
        public LogEntry(string threadID, LogSeverity severity, Type type, Exception exception)
            : this(DateTime.Now, threadID, severity, type, exception.Message, exception) { }

        public LogEntry(LogSeverity severity, Type type, string message) : this(ThreadName(Thread.CurrentThread), severity, type, message, null) { }
        public LogEntry(LogSeverity severity, Type type, string message, Exception exception) : this(ThreadName(Thread.CurrentThread), severity, type, message, exception) { }
        public LogEntry(LogSeverity severity, Type type, Exception exception) : this(ThreadName(Thread.CurrentThread), severity, type, exception.Message, exception) { }
        #else
        public LogEntry(DateTime timestamp, LogSeverity severity, Type type, string message) : this(timestamp, severity, type, message, null) { }
        public LogEntry(DateTime timestamp, LogSeverity severity, Type type, Exception exception) : this(timestamp, severity, type, exception.Message, exception) { }
        public LogEntry(DateTime timestamp, LogSeverity severity, Type type, string message, Exception exception)
        {
            _timestamp = timestamp;
            _severity = severity;
            _type = type;
            if (exception != null && string.IsNullOrEmpty(message))
            {
                _message = exception.Message;
            }
            else
            {
                _message = message;
            }
            _exception = exception;
        }

        public LogEntry(LogSeverity severity, Type type, string message) : this(DateTime.Now, severity, type, message, null) { }
        public LogEntry(LogSeverity severity, Type type, string message, Exception exception) : this(DateTime.Now, severity, type, message, exception) { }
        public LogEntry(LogSeverity severity, Type type, Exception exception) : this(DateTime.Now, severity, type, exception.Message, exception) { }
        #endif

        public override string ToString()
        {
            var messageToWrite = _exception == null ? _message : $"{_message}\n{_exception.StackTrace}";
            #if NETSTANDARD1_6_OR_NEWER || !NETSTANDARD
            return $"{_timestamp:yyyy-MM-dd HH:mm:ss} {_threadID} [{_severity}] {_type.FullName}: {messageToWrite}";
            #else
            return $"{_timestamp:yyyy-MM-dd HH:mm:ss} [{_severity}] {_type.FullName}: {messageToWrite}";
            #endif
        }

        public DateTime Timestamp => _timestamp;
        #if NETSTANDARD1_6_OR_NEWER || !NETSTANDARD
        public string ThreadID => _threadID;
        #endif
        public LogSeverity Severity => _severity;
        public Type Type => _type;
        public string Message => _message;
        public Exception Exception => _exception;
    }
}