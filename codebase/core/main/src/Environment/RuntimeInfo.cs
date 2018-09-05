#if NETSTANDARD || NET35_OR_NEWER
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
#if NETFRAMEWORK
using System.IO;
using System.Security.Policy;
#endif
#if NETSTANDARD1_7
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyModel;
#endif

using Axle.Conversion.Parsing;
using Axle.Extensions.String;
using Axle.Verification;


namespace Axle.Environment
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    internal sealed class RuntimeInfo : IRuntime
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
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

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
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
            #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
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

        #if NETFRAMEWORK
        public Assembly LoadAssembly(string assemblyName) { return LoadAssembly(assemblyName, null); }
        public Assembly LoadAssembly(string assemblyName, Evidence securityEvidence)
        {
            if (assemblyName == null)
            {
                throw new ArgumentNullException(nameof(assemblyName));
            }
            if (assemblyName.Length == 0)
            {
                throw new ArgumentException("Assembly name cannot be an empty string,", nameof(assemblyName));
            }

            assemblyName = ResolveAssemblyName(assemblyName);

            const string ext = ".dll";
            var hasExt = assemblyName.EndsWith(ext, StringComparison.OrdinalIgnoreCase);

            ICollection<Attempt<string, Evidence, Assembly>> chain = new List<Attempt<string, Evidence, Assembly>>(4)
                {
                    (string a, Evidence e, out Assembly res) =>
                    {
                        try
                        {
                            res = e == null ? Assembly.Load(a) : Assembly.Load(a, e);
                            return res != null;
                        }
                        catch
                        {
                            res = null;
                            return false;
                        }
                    },
                    (string a, Evidence e, out Assembly res) =>
                    {
                        var name = !hasExt ? a : a + ext;
                        try
                        {
                            res = e == null ? Assembly.LoadFrom(name) : Assembly.LoadFrom(name, e);
                            return res != null;
                        }
                        catch
                        {
                            res = null;
                            return false;
                        }
                    },
                    (string a, Evidence e, out Assembly res) =>
                    {
                        var name = Path.Combine(Directory.GetCurrentDirectory(), hasExt ? a : a + ext);
                        try
                        {
                            res = e == null ? Assembly.LoadFrom(name) : Assembly.LoadFrom(name, e);
                            return res != null;
                        }
                        catch
                        {
                            res = null;
                            return false;
                        }
                    }
                };

            var noExtName = hasExt ? assemblyName.TakeBeforeLast('.') : assemblyName;
            if (chain.Any(noExtName, securityEvidence, out var result))
            {
                return result;
            }
            throw new ArgumentException("Unable to load assembly, the given assembly name is invalid: '" + assemblyName + "'", nameof(assemblyName));
        }
        #else
        public IEnumerable<Assembly> GetReferencingAssemblies(string assemblyName)
        {
            var assemblies = new List<Assembly>();
            #if NETSTANDARD1_7
            //foreach (CompilationLibrary compilationLibrary in DependencyContext.Default.CompileLibraries)

            //var dependencies = AssemblyLoadContext.Default.GetLoadedAssemblies();
            //var dependencies = DependencyContext.Default.RuntimeLibraries;
            //foreach (var library in dependencies)
            //{
            //    if (IsCandidateCompilationLibrary(library, assemblyName))
            //    {
            //        var assembly = Assembly.Load(new AssemblyName(library.Name));
            //        assemblies.Add(assembly);
            //    }
            //}
            #endif
            return assemblies.ToArray();
        }
        #if NETSTANDARD1_7
        private static bool IsCandidateCompilationLibrary(RuntimeLibrary compilationLibrary, string assemblyName)
        {
            var cmp = StringComparison.OrdinalIgnoreCase;
            return compilationLibrary.Name.Equals(assemblyName, cmp) || compilationLibrary.Dependencies.Any(d => d.Name.StartsWith(assemblyName, cmp));
        }
        #endif

        public Assembly LoadAssembly(string assemblyName)
        {
            if (assemblyName == null)
            {
                throw new ArgumentNullException(nameof(assemblyName));
            }
            if (assemblyName.Length == 0)
            {
                throw new ArgumentException("Assembly name cannot be an empty string,", nameof(assemblyName));
            }

            //assemblyName = ResolveAssemblyName(assemblyName);

            const string ext = ".dll";
            var hasExt = assemblyName.EndsWith(ext, StringComparison.OrdinalIgnoreCase);

            ICollection<Attempt<string, Assembly>> chain = new List<Attempt<string, Assembly>>(4)
                {
                    (string a, out Assembly res) =>
                    {
                        try
                        {
                            res = Assembly.Load(new AssemblyName(a));
                            return res != null;
                        }
                        catch
                        {
                            res = null;
                            return false;
                        }
                    },
                    //(string a, out Assembly res) =>
                    //{
                    //    var name = !hasExt ? a : a + ext;
                    //    try
                    //    {
                    //        res = Assembly.LoadFrom(name);
                    //        return res != null;
                    //    }
                    //    catch
                    //    {
                    //        res = null;
                    //        return false;
                    //    }
                    //},
                    //(string a, out Assembly res) =>
                    //{
                    //    var name = Path.Combine(Directory.GetCurrentDirectory(), hasExt ? a : a + ext);
                    //    try
                    //    {
                    //        res = Assembly.LoadFrom(name);
                    //        return res != null;
                    //    }
                    //    catch
                    //    {
                    //        res = null;
                    //        return false;
                    //    }
                    //}
                };

            var noExtName = hasExt ? assemblyName.TakeBeforeLast('.') : assemblyName;
            if (chain.Any(noExtName, out var result))
            {
                return result;
            }
            throw new ArgumentException("Unable to load assembly, the given assembly name is invalid: '" + assemblyName + "'", nameof(assemblyName));
        }
        #endif

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
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
                .Select(x => System.IO.Path.Combine(x, $"{culture.Name}/{targetAssembly.GetName().Name}.resources.dll"))
                .FirstOrDefault(System.IO.File.Exists);
            return satelliteAssemblyPath != null 
                ? LoadAssembly(System.IO.Path.GetFullPath(satelliteAssemblyPath)) 
                : null;
        }
        #endif

        public Version Version => _version;
        public Version FrameworkVersion => _frameworkVersion;
        public RuntimeImplementation Implementation => _impl;

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        public AppDomain Domain => AppDomain.CurrentDomain;
        #endif
    }
}
#endif