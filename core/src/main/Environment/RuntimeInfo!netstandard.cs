using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;

using Axle.Conversion.Parsing;
using Axle.Extensions.String;
using Axle.Verification;


namespace Axle.Environment
{
    [Serializable]
    partial class RuntimeInfo
    {
        private static Version GetMonoVersion()
        {
            var dispalayNameMethod = Type.GetType("Mono.Runtime")?.GetMethod("GetDisplayName", BindingFlags.NonPublic | BindingFlags.Static);
            return dispalayNameMethod != null
                ? new VersionParser().Parse(dispalayNameMethod.Invoke(null, null).ToString().TakeBeforeFirst(" ", StringComparison.OrdinalIgnoreCase).Trim())
                : null;
        }

        internal RuntimeInfo()
        {
            var monoVersion = GetMonoVersion();
            this.frameworkVersion = System.Environment.Version;
            this.version = monoVersion ?? frameworkVersion;
            this.impl = monoVersion != null ? RuntimeImplementation.Mono : RuntimeImplementation.NetFramework;
        }

        public IEnumerable<Assembly> GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }

        private string ResolveAssemblyName(string assemblyName)
        {
            return !Platform.Environment.IsWindows()
                ? GetAssemblies()
                      .Select(x => x.GetName().Name)
                      .Where(x => x.Equals(assemblyName, StringComparison.OrdinalIgnoreCase))
                      .SingleOrDefault() ?? assemblyName
                : assemblyName;
        }

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

        public Assembly LoadSatelliteAssembly(Assembly targetAssembly, CultureInfo culture)
        {
            targetAssembly.VerifyArgument(nameof(targetAssembly)).IsNotNull();
            culture.VerifyArgument(nameof(culture)).IsNotNull();

            if (culture.Equals(CultureInfo.InvariantCulture))
            {
                return null;
            }

            var paths = new[] { Domain.RelativeSearchPath, Domain.BaseDirectory };
            var satelliteAssemblyPath = paths
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(
                    x =>
                    {
                        var path = Path.Combine(x, $"{culture.Name}/{targetAssembly.GetName().Name}.resources.dll");
                        return File.Exists(path) ? path : null;
                    })
                .FirstOrDefault(x => x != null);
            return satelliteAssemblyPath != null ? LoadAssembly(Path.GetFullPath(satelliteAssemblyPath)) : null;
        }

        public AppDomain Domain => AppDomain.CurrentDomain;
    }
}