#if NETSTANDARD || NET35_OR_NEWER
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

        public static bool IsMac(this IEnvironment environment) => IsOS(environment, OperatingSystemID.Mac);

        #if NETSTANDARD
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        public static bool IsOS(this IEnvironment environment, OperatingSystemID osID)
        {
            environment.VerifyArgument(nameof(environment)).IsNotNull();
            return IsOSInternal(environment, osID);
        }
        public static bool IsUnix(this IEnvironment environment) => IsOS(environment, OperatingSystemID.Unix);

        public static bool IsWindows(this IEnvironment environment) => IsOS(environment, OperatingSystemID.Windows);
        #endif
    }
}
#endif