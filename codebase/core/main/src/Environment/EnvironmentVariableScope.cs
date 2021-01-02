namespace Axle.Environment
{
    /// <summary>
    /// An enumeration representing the scope of an environment variable.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [System.Serializable]
    #endif
    public enum EnvironmentVariableScope : byte
    {
        /// <summary>
        /// Used for environment variables defined specifically for the current process.
        /// </summary>
        Process = 0,
        /// <summary>
        /// Used for environment variables defined for the current operating system user.
        /// </summary>
        User = 1,
        /// <summary>
        /// Used for environment variables defined on a system-wide level.
        /// </summary>
        System = 2
    }
}