namespace Axle.Logging
{
    /// <summary>
    /// An enumeration that represents the importance (severity) of an application log's entries.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public enum LogSeverity : sbyte
    {
        /// <summary>
        /// The entry is logged for debugging purposes.
        /// </summary>
        Debug = -1,

        /// <summary>
        /// The entry is logged for informative purposes.
        /// </summary>
        Info = 0,

        /// <summary>
        /// The entry represents a warning. A warning indicates unexpected state detected by the application at runtime
        /// which can be worked around and the application would continue to operate.
        /// </summary>
        Warning = 1,

        /// <summary>
        /// The entry represents an error.
        /// </summary>
        Error = 2,

        /// <summary>
        /// The entry represents a fatal condition that causes the application to terminate unexpectedly.
        /// </summary>
        Fatal = 3   
    }
}