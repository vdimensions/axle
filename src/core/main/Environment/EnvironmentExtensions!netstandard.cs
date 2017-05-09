using Axle.Verification;


namespace Axle.Environment
{
    partial class EnvironmentExtensions
    {
        private static bool IsOSInternal(IEnvironment environment, OperatingSystemID osID)
        {
            return (environment.OperatingSystemID & osID) == osID;
        }

        public static bool IsMac(this IEnvironment environment) { return IsOS(environment, OperatingSystemID.Mac); }
        public static bool IsOS(this IEnvironment environment, OperatingSystemID osID)
        {
            return IsOSInternal(environment.VerifyArgument("environment").IsNotNull().Value, osID);
        }
        public static bool IsUnix(this IEnvironment environment) { return IsOS(environment, OperatingSystemID.Unix); }
        public static bool IsWindows(this IEnvironment environment) { return IsOS(environment, OperatingSystemID.Windows); }
    }
}