using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

using Axle.Conversion.Parsing;
using Axle.Extensions.String;
using Axle.Verification;


namespace Axle.Environment
{
    internal sealed partial class RuntimeInfo : IRuntime
    {
        #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
        private static Version GetMonoVersion()
        {
            var dispalayNameMethod = Type.GetType("Mono.Runtime")?.GetMethod("GetDisplayName", BindingFlags.NonPublic | BindingFlags.Static);
            return dispalayNameMethod != null
                ? new VersionParser().Parse(dispalayNameMethod.Invoke(null, null).ToString().TakeBeforeFirst(" ", StringComparison.OrdinalIgnoreCase).Trim())
                : null;
        }
        #endif

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Version version;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Version frameworkVersion;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly RuntimeImplementation impl = RuntimeImplementation.Unknown;

        #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
        internal RuntimeInfo()
        {
            var monoVersion = GetMonoVersion();
            this.frameworkVersion = System.Environment.Version;
            this.version = monoVersion ?? frameworkVersion;
            this.impl = monoVersion != null ? RuntimeImplementation.Mono : RuntimeImplementation.NetFramework;
        }
        #endif

        public IEnumerable<Assembly> GetAssemblies()
        {
            #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
            return AppDomain.CurrentDomain.GetAssemblies();
            #else
            var assemblies = new List<Assembly>();
            #if NETSTANDARD1_6
            //var dependencies = DependencyContext.Default.RuntimeLibraries;
            //foreach (var library in dependencies)
            //{
            //    var assembly = Assembly.Load(new AssemblyName(library.Name));
            //    assemblies.Add(assembly);
            //}
            #endif
            return assemblies.ToArray();
            #endif
        }

        #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
        private string ResolveAssemblyName(string assemblyName)
        {
            return !Platform.Environment.IsWindows()
                    ? GetAssemblies()
                      .Select(x => x.GetName().Name)
                      .Where(x => x.Equals(assemblyName, StringComparison.OrdinalIgnoreCase))
                      .SingleOrDefault() ?? assemblyName
                    : assemblyName;
        }
        #endif

        public string GetEmbeddedResourcePath(string resourceName)
        {
            return resourceName.VerifyArgument(nameof(resourceName)).IsNotNull().Value.Replace(" ", "_").Replace("-", "_").Replace("\\", ".").Replace("/", ".");
        }

        public Version Version => version;
        public Version FrameworkVersion => frameworkVersion;
        public RuntimeImplementation Implementation => impl;

        #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
        public AppDomain Domain => AppDomain.CurrentDomain;
        #endif
    }
}