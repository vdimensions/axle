using Axle.Verification;


namespace Axle.Environment
{
    public static partial class EnvironmentExtensions
    {
        #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
        private static bool IsOSInternal(IEnvironment environment, OperatingSystemID osID)
        {
            return (environment.OperatingSystemID & osID) == osID;
        }

        public static bool IsMac(this IEnvironment environment) { return IsOS(environment, OperatingSystemID.Mac); }
        public static bool IsOS(this IEnvironment environment, OperatingSystemID osID)
        {
            return IsOSInternal(environment.VerifyArgument(nameof(environment)).IsNotNull().Value, osID);
        }
        public static bool IsUnix(this IEnvironment environment) { return IsOS(environment, OperatingSystemID.Unix); }
        public static bool IsWindows(this IEnvironment environment) { return IsOS(environment, OperatingSystemID.Windows); }
        #endif
    }
}