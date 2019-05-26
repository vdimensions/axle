#if NETSTANDARD || NET20_OR_NEWER
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
#if NETFRAMEWORK
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
            var displayNameMethod = Type.GetType("Mono.Runtime")?.GetMethod("GetDisplayName", BindingFlags.NonPublic | BindingFlags.Static);
            return displayNameMethod != null
                ? new VersionParser().Parse(StringExtensions.TakeBeforeFirst(displayNameMethod.Invoke(null, null).ToString(), " ", StringComparison.OrdinalIgnoreCase).Trim())
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
        public Assembly LoadAssembly(string assemblyName) => LoadAssembly(assemblyName, null);
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
                        var name = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), hasExt ? a : a + ext);
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

            var noExtName = hasExt ? StringExtensions.TakeBeforeLast(assemblyName, '.') : assemblyName;
            foreach (var attempt in chain)
            {
                if (attempt.Invoke(noExtName, securityEvidence, out var result))
                {
                    return result;
                }
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

            var noExtName = hasExt ? StringExtensions.TakeBeforeLast(assemblyName, '.') : assemblyName;
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
            if (!EnvironmentExtensions.IsWindows(Platform.Environment))
            {
                foreach (var assembly in GetAssemblies())
                {
                    var resolveAssemblyName = assembly.GetName().Name;
                    if (resolveAssemblyName.Equals(assemblyName, StringComparison.OrdinalIgnoreCase))
                    {
                        return resolveAssemblyName;
                    }
                }
            }
            return assemblyName;
        }

        public Assembly LoadSatelliteAssembly(Assembly targetAssembly, System.Globalization.CultureInfo culture)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(targetAssembly, nameof(targetAssembly)));
            Verifier.IsNotNull(Verifier.VerifyArgument(culture, nameof(culture)));

            if (culture.Equals(System.Globalization.CultureInfo.InvariantCulture))
            {
                return null;
            }

            var paths = new[] { Domain.RelativeSearchPath, Domain.BaseDirectory };
            var satelliteAssemblyDllName = $"{culture.Name}/{targetAssembly.GetName().Name}.resources.dll";
            foreach (var path in paths)
            {
                if (string.IsNullOrEmpty(path))
                {
                    continue;
                }
                var satelliteAssemblyPath = System.IO.Path.Combine(path, satelliteAssemblyDllName);
                if (!System.IO.File.Exists(satelliteAssemblyPath))
                {
                    continue;
                }
                return LoadAssembly(System.IO.Path.GetFullPath(satelliteAssemblyPath));
            }

            return null;
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