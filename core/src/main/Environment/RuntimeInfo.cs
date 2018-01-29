using System;
using System.Diagnostics;

using Axle.Verification;


namespace Axle.Environment
{
    internal sealed partial class RuntimeInfo : IRuntime
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Version version;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Version frameworkVersion;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly RuntimeImplementation impl = RuntimeImplementation.Unknown;

        public string GetEmbeddedResourcePath(string resourceName)
        {
            return resourceName.VerifyArgument(nameof(resourceName)).IsNotNull().Value.Replace(" ", "_").Replace("-", "_").Replace("\\", ".").Replace("/", ".");
        }

        public Version Version => version;
        public Version FrameworkVersion => frameworkVersion;
        public RuntimeImplementation Implementation => impl;
    }
}