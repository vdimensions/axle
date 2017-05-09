using System;
using System.Diagnostics;
using System.Linq;
using Axle.Verification;


namespace Axle.Environment
{
    internal sealed partial class RuntimeInfo : IRuntime
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Version version;

        public string GetEmbeddedResourcePath(string resourceName)
        {
            return resourceName.VerifyArgument(nameof(resourceName)).IsNotNull().Value.Replace(" ", "_").Replace("-", "_").Replace("\\", ".").Replace("/", ".");
        }

        public Version Version { get { return version; } }
    }
}