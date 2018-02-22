using System;
using System.Diagnostics;
using System.Threading;


namespace Axle.Logging
{
    /// <summary>
    /// The default <see cref="ILogEntry"/> implementation provided by the Axle Framework.
    /// </summary>
    #if !NETSTANDARD
    [Serializable]
    #endif
    public sealed class LogEntry : ILogEntry
    {
        private static string ThreadName(Thread t) => t.Name ?? $"Thread-{t.ManagedThreadId}";

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DateTime _timestamp;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string _threadID;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly LogSeverity _severity;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Type _type;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string _message;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Exception _exception;

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
        public LogEntry(LogSeverity severity, Type type, string message)
            : this(DateTime.Now, ThreadName(Thread.CurrentThread), severity, type, message, null) { }
        public LogEntry(LogSeverity severity, Type type, string message, Exception exception)
            : this(DateTime.Now, ThreadName(Thread.CurrentThread), severity, type, message, exception) { }
        public LogEntry(LogSeverity severity, Type type, Exception exception)
            : this(DateTime.Now, ThreadName(Thread.CurrentThread), severity, type, exception.Message, exception) { }

        public override string ToString()
        {
            var messageToWrite = _exception == null ? _message : string.Format("{0}\n{1}", _message, _exception.StackTrace);
            return string.Format(
                "{0:yyyy-MM-dd HH:mm:ss} {1} [{2}] {3}: {4}", 
                _timestamp, 
                _threadID, 
                _severity, 
                _type.FullName, 
                messageToWrite);
        }

        public DateTime Timestamp => _timestamp;
        public string ThreadID => _threadID;
        public LogSeverity Severity => _severity;
        public Type Type => _type;
        public string Message => _message;
        public Exception Exception => _exception;
    }
}