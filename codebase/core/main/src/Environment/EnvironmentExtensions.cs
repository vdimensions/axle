#if NETSTANDARD || NET20_OR_NEWER
using Axle.Verification;


namespace Axle.Environment
{
    public static class EnvironmentExtensions
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        #if NETSTANDARD
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        private static bool IsOSInternal(IEnvironment environment, OperatingSystemID osID)
        {
            return (environment.OperatingSystemID & osID) == osID;
        }

        public static bool IsMac(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            IEnvironment environment) => IsOS(environment, OperatingSystemID.Mac);

        #if NETSTANDARD
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        public static bool IsOS(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            IEnvironment environment, OperatingSystemID osID)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(environment, nameof(environment)));
            return IsOSInternal(environment, osID);
        }
        public static bool IsUnix(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            IEnvironment environment) => IsOS(environment, OperatingSystemID.Unix);

        public static bool IsWindows(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            IEnvironment environment) => IsOS(environment, OperatingSystemID.Windows);
        #endif
    }
}
#endif