using System;


namespace Axle.Reflection
{
    /// <summary>
    /// An enumeration with all the possible flags that can be used when reflecting a type member with an <see cref="IIntrospector" >introspector</see> instance
    /// </summary>
    /// <seealso cref="IIntrospector"/>
    #if !NETSTANDARD
    [Serializable]
    #endif
    [Flags]
    public enum ScanOptions
    {
        /// <summary>
        /// Scan results will include nothing.
        /// </summary>
        None = 0,
        /// <summary>
        /// Scan results will include matching public members.
        /// </summary>
        Public = 1,
        /// <summary>
        /// Scan results will include matching non-public members.
        /// </summary>
        NonPublic = 1 << 1,
        /// <summary>
        /// Scan results will include matching static members.
        /// </summary>
        Static = 1 << 2,
        /// <summary>
        /// Scan results will include matching instance members.
        /// </summary>
        Instance = 1 << 3,
        /// <summary>
        /// Scan results will include matching public instance members. 
        /// <para>
        /// This is a shortcut for using the <c>ScanOptions.Public | ScanOptions.Instance</c> flags.
        /// </para>
        /// </summary>
        PublicInstance = Public | Instance,
        /// <summary>
        /// Scan results will include matching public static members. 
        /// <para>
        /// This is a shortcut for using the <c>ScanOptions.Public | ScanOptions.Static</c> flags.
        /// </para>
        /// </summary>
        PublicStatic = Public | Static,

        /// <summary>
        /// Scan results will include matching public static and instance members.
        /// </summary>
        Default = Public | Static | Instance
    }
}