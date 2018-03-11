namespace Axle.Core.Infrastructure.Logging
{
    /// <summary>
    /// An enumeration that represents the importance (severity) of an application log's entries.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    [System.Serializable]
    #endif
    public enum LogSeverity : byte
    {
        None = 0,

        /// <summary>
        /// The entry is logged for debugging purposes.
        /// </summary>
        Debug = 1,

        /// <summary>
        /// The entry is logged for informative purposes. Usually used to indicate that certain application event occurred, without signifying its importance.
        /// </summary>
        Info = 2,

        /// <summary>
        /// Same as <see cref="Info">Info</see>
        /// </summary>
        /// <seealso cref="Info">Info</seealso>
        Trace = Info,

        /// <summary>
        /// The entry represents a warning. Usually, a warning is an error event that normally should not occur, but the application has a way to work it 
        /// around and continue to operate.
        /// </summary>
        Warning = 3,

        /// <summary>
        /// The entry represents an error.
        /// </summary>
        Error = 4,

        /// <summary>
        /// The entry represents a fatal condition that causes the application to terminate unexpectedly.
        /// </summary>
        Failure = 5,

        /// <summary>
        /// Same as <see cref="Failure">Failure</see>
        /// </summary>
        /// <seealso cref="Failure">Failure</seealso>
        Fatal = Failure       
    }
}