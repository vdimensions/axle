#if NETSTANDARD || NET20_OR_NEWER || UNITY_2018_1_OR_NEWER
using Axle.Verification;


namespace Axle.Environment
{
    /// <summary>
    /// A <see langword="static"/> class containing extension methods for 
    /// the <see cref="IEnvironment"/> interface
    /// </summary>
    public static class EnvironmentExtensions
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
        #if NETSTANDARD || UNITY_2018_1_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        private static bool IsOSInternal(IEnvironment environment, OperatingSystemID osID)
        {
            return (environment.OperatingSystemID & osID) == osID;
        }

        /// <summary>
        /// Determines whether the given <paramref name="environment"/> object represents
        /// a <see cref="OperatingSystemID.MacOS"/> operating system.
        /// </summary>
        /// <param name="environment">
        /// The <see cref="IEnvironment"/> instance to check.
        /// </param>
        /// <returns>
        /// <c><see langword="true"/></c> if the given <paramref name="environment"/> is 
        /// a <see cref="OperatingSystemID.MacOS"/> operating system;
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        /// <seealso cref="IsOS(IEnvironment, OperatingSystemID)"/>
        /// <seealso cref="OperatingSystemID"/>
        public static bool IsMac(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            IEnvironment environment) => IsOS(environment, OperatingSystemID.Mac);

        /// <summary>
        /// Determines whether the given <paramref name="environment"/> object represents
        /// a certain operating system.
        /// </summary>
        /// <param name="environment">
        /// The <see cref="IEnvironment"/> instance to check.
        /// </param>
        /// <param name="osID">
        /// The <see cref="OperatingSystemID"/> of the operating system to check.
        /// </param>
        /// <returns>
        /// <c><see langword="true"/></c> if the given <paramref name="environment"/> is 
        /// an operating system represented by the <paramref name="osID"/>;
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        /// <seealso cref="OperatingSystemID"/>
        #if NETSTANDARD || UNITY_2018_1_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        public static bool IsOS(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            IEnvironment environment, OperatingSystemID osID)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(environment, nameof(environment)));
            return IsOSInternal(environment, osID);
        }
        /// <summary>
        /// Determines whether the given <paramref name="environment"/> object represents
        /// a <see cref="OperatingSystemID.Unix"/> operating system.
        /// </summary>
        /// <param name="environment">
        /// The <see cref="IEnvironment"/> instance to check.
        /// </param>
        /// <returns>
        /// <c><see langword="true"/></c> if the given <paramref name="environment"/> is 
        /// a <see cref="OperatingSystemID.Unix"/> operating system;
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        /// <seealso cref="IsOS(IEnvironment, OperatingSystemID)"/>
        /// <seealso cref="OperatingSystemID"/>
        public static bool IsUnix(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            IEnvironment environment) => IsOS(environment, OperatingSystemID.Unix);

        /// <summary>
        /// Determines whether the given <paramref name="environment"/> object represents
        /// a <see cref="OperatingSystemID.Windows"/> operating system.
        /// </summary>
        /// <param name="environment">
        /// The <see cref="IEnvironment"/> instance to check.
        /// </param>
        /// <returns>
        /// <c><see langword="true"/></c> if the given <paramref name="environment"/> is 
        /// a <see cref="OperatingSystemID.Windows"/> operating system;
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        /// <seealso cref="IsOS(IEnvironment, OperatingSystemID)"/>
        /// <seealso cref="OperatingSystemID"/>
        public static bool IsWindows(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            IEnvironment environment) => IsOS(environment, OperatingSystemID.Windows);
        #endif
    }
}
#endif