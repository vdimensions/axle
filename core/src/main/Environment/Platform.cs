using System.Diagnostics;

using Axle.References;


namespace Axle.Environment
{
    /// <summary>
    /// A static class that enables access to the current execution environment and .NET runtime properties.
    /// </summary>
    public static partial class Platform
    {
        #if NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4
        private static readonly IEnvironment _env = new EnvironmentInfo();
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly IRuntime _runtime = new RuntimeInfo();
        #else
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly IEnvironment _env = Singleton<EnvironmentInfo>.Instance.Value;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly IRuntime _runtime = Singleton<RuntimeInfo>.Instance.Value;
        #endif

        static Platform() { }

        /// <summary>
        /// Gets an <see cref="IEnvironment"/> instance that represents the current execution environment.
        /// </summary>
        public static IEnvironment Environment { get { return _env; } }
        /// <summary>
        /// Gets an instance of <see cref="IRuntime"/> that represents the current .NET runtime.
        /// </summary>
        public static IRuntime Runtime { get { return _runtime; } }
    }
}
