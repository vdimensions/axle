using System;
using System.Collections.Generic;
using System.Reflection;

#if NETSTANDARD1_7
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyModel;
#endif

using Axle.Extensions.String;


namespace Axle.Environment
{
    internal sealed partial class RuntimeInfo
    {
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
            if (chain.Any(noExtName, out Assembly result))
            {
                return result;
            }
            throw new ArgumentException("Unable to load assembly, the given assembly name is invalid: '" + assemblyName + "'", nameof(assemblyName));
        }
    }
}