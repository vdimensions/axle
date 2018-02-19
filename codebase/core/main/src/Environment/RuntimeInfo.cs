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
        private readonly Version _version;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Version _frameworkVersion;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly RuntimeImplementation _impl = RuntimeImplementation.Unknown;

        #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
        internal RuntimeInfo()
        {
            var monoVersion = GetMonoVersion();
            _frameworkVersion = System.Environment.Version;
            _version = monoVersion ?? _frameworkVersion;
            _impl = monoVersion != null ? RuntimeImplementation.Mono : RuntimeImplementation.NetFramework;
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

        public Assembly LoadSatelliteAssembly(Assembly targetAssembly, System.Globalization.CultureInfo culture)
        {
            targetAssembly.VerifyArgument(nameof(targetAssembly)).IsNotNull();
            culture.VerifyArgument(nameof(culture)).IsNotNull();

            if (culture.Equals(System.Globalization.CultureInfo.InvariantCulture))
            {
                return null;
            }

            var paths = new[] { Domain.RelativeSearchPath, Domain.BaseDirectory };
            var satelliteAssemblyPath = paths
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(
                    x =>
                    {
                        var path = System.IO.Path.Combine(x, $"{culture.Name}/{targetAssembly.GetName().Name}.resources.dll");
                        return System.IO.File.Exists(path) ? path : null;
                    })
                .FirstOrDefault(x => x != null);
            return satelliteAssemblyPath != null 
                ? LoadAssembly(System.IO.Path.GetFullPath(satelliteAssemblyPath)) 
                : null;
        }
        #endif

        public Version Version => _version;
        public Version FrameworkVersion => _frameworkVersion;
        public RuntimeImplementation Implementation => _impl;

        #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
        public AppDomain Domain => AppDomain.CurrentDomain;
        #endif
    }
}