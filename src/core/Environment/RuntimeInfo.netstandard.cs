using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
#if NETSTANDARD1_5 || NETSTANDARD1_6
using System.Runtime.Loader;
#endif

using Axle.Extensions;
using Axle.Extensions.String;
using Axle.Verification;


namespace Axle.Environment
{
    internal sealed partial class RuntimeInfo
    {
        public IEnumerable<Assembly> GetAssemblies()
        {
            var assemblies = new List<Assembly>();
#if NETSTANDARD1_5 || NETSTANDARD1_6
//            var dependencies = DependencyContext.Default.RuntimeLibraries;
//            foreach (var library in dependencies)
//            {
//                if (IsCandidateCompilationLibrary(library))
//                {
//                    var assembly = Assembly.Load(new AssemblyName(library.Name));
//                    assemblies.Add(assembly);
//                }
//            }
#endif
            return assemblies.ToArray();
        }
#if NETSTANDARD1_5 || NETSTANDARD1_6
        //        private static bool IsCandidateCompilationLibrary(RuntimeLibrary compilationLibrary)
        //        {
        //            return compilationLibrary.Name == "Specify" || compilationLibrary.Dependencies.Any(d => d.Name.StartsWith("Specify"));
        //        }
#endif

#warning Stub method
        public Assembly LoadAssembly(string assemblyName)
        {
            return null;
        }
    }
}