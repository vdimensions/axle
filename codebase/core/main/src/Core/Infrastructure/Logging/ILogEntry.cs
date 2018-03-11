using System;


namespace Axle.Core.Infrastructure.Logging
{
    /// <summary>
    /// Represents a log entry; that is, a representation of an application event that is to be written to the application log.
    /// </summary>
    public interface ILogEntry
    {
        /// <summary>
        /// The exact date and time of the occurrence of the application event.
        /// </summary>
        DateTime Timestamp { get; }

        #if NETSTANDARD1_6_OR_NEWER || !NETSTANDARD

        /// <summary>
        /// The name of the thread where the application event occurred.
        /// </summary>
        string ThreadID { get; }
        #endif

        /// <summary>
        /// The <see cref="LogSeverity">severity</see> of the application event.
        /// </summary>
        LogSeverity Severity { get; }

        /// <summary>
        /// The <see cref="Type">type</see> of the object that triggered the event.
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// A custom message describing the log event. In case of an <see cref="System.Exception">exception</see>, this could represent 
        /// the actual <see cref="System.Exception.Message">exception message</see>.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Holds a reference to the <see cref="System.Exception">exception</see> instance that may be the cause of the event,
        /// or <c>null</c> in case of non-exceptional application event.
        /// </summary>
        Exception Exception { get; }
    }
}