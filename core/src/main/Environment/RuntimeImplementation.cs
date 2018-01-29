namespace Axle.Environment
{
    /// <summary>
    /// An enumeration describing the possible type of CLR implementations.
    /// </summary>
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public enum RuntimeImplementation
    {
        /// <summary>
        /// Unrecognized CLR implementation.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Mono is a free, open-source implementation of the CLR.
        /// </summary>
        Mono,
        /// <summary>
        /// The standard .NET framework CLR from Microsoft.
        /// </summary>
        NetFramework,
        /// <summary>
        /// .NET Core
        /// </summary>
        NetCore,
        /// <summary>
        /// .NET Standard
        /// </summary>
        NetStandard
    }
}