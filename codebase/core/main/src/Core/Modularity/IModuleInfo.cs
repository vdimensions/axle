using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Axle.Extensions.String;
using Axle.Reflection;
using Axle.Verification;


namespace Axle.Core.Modularity
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ModuleAttribute : Attribute
    {
        public ModuleAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public abstract class ModuleCallbackAttribute : Attribute
    {
        protected ModuleCallbackAttribute(bool allowParallelInvoke) { AllowParallelInvoke = allowParallelInvoke; }
        protected ModuleCallbackAttribute() : this(false) { }

        public bool AllowParallelInvoke { get; set; }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class ModuleInitMethodAttribute : ModuleCallbackAttribute { }

    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class ModuleEntryPointAttribute : ModuleCallbackAttribute { }

    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class ModuleOnDependencyInitializedAttribute : ModuleCallbackAttribute { }

    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class ModuleOnDependenciesReadyAttribute : ModuleCallbackAttribute { }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnModuleAttribute : Attribute
    {
        public DependsOnModuleAttribute(Type moduleType)
        {
            ModuleType = moduleType;
        }

        public Type ModuleType { get; }
    }

    public class ModuleInfo
    {
        public ModuleInfo(Type type, string name, IInvokable initMethod, IInvokable onDependencyInitializedMethod, IInvokable onDependenciesReadyMethod, IInvokable entryPointMethod, params ModuleInfo[] requiredModules)
        {
            Type = type.VerifyArgument(nameof(type)).IsNotAbstract();
            Name = name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();
            InitMethod = initMethod;
            OnDependencyInitializedMethod = onDependencyInitializedMethod;
            OnDependenciesReadyMethod = onDependenciesReadyMethod;
            EntryPointMethod = entryPointMethod;
            RequiredModules = requiredModules;
        }

        public string Name { get; }
        public Type Type { get; }
        public Assembly Assembly => Type.Assembly;
        public IInvokable InitMethod { get; }
        public IInvokable OnDependencyInitializedMethod { get; }
        public IInvokable OnDependenciesReadyMethod { get; }
        public IInvokable EntryPointMethod { get; }
        public IEnumerable<ModuleInfo> RequiredModules { get; }
    }

    public interface IModuleExporter
    {
        IModuleExporter Export<T>(T instance, string name);
        IModuleExporter Export<T>(T instance);
    }

    /// <summary>
    /// A class representing the module shepard; that is, a special module which is defined as depending on all modules in 
    /// a module catalog. Its purpose is to guarantee all of those modules are initialized before the application is ran,
    /// as well as to collect statistical information on which modules are being loaded.
    /// </summary>
    [Module(Name)]
    internal sealed class ModuleShepard : IDisposable
    {
        public const string Name = "<ModuleShepard>";

        [ModuleInitMethod(AllowParallelInvoke = false)]
        public void Init() { }

        [ModuleOnDependencyInitialized(AllowParallelInvoke = false)]
        public void OnDependencyInitialized(object dependency) { }

        [ModuleOnDependenciesReady(AllowParallelInvoke = false)]
        public void OnDependenciesReady() { }

        [ModuleEntryPoint(AllowParallelInvoke = false)]
        public void Run(params string[] args) { }

        public void Dispose() { }
    }

    public class ModuleCatalog
    {
        /// <summary>
        /// Creates a list of ranked modules, grouped by and sorted by a rank integer. The rank determines
        /// the order for bootstrapping the modules -- those with lower rank will be bottstrapped earlier. 
        /// A module with particular rank does not have module dependencies with a higher rank. 
        /// Modules of the same rank can be bootstrapped simultaneously in multiple threads, if parallel option is specified.
        /// </summary>
        /// <param name="moduleCatalog">
        /// An collection of modules to be ranked.
        /// </param>
        /// <returns>
        /// A collection of module groups, sorted by rank. 
        /// </returns>
        private static IEnumerable<IGrouping<int, ModuleInfo>> RankModules(IEnumerable<ModuleInfo> moduleCatalog)
        {
            var stringComparer = StringComparer.Ordinal;
            IDictionary<string, Tuple<ModuleInfo, int>> modulesWithRank = new Dictionary<string, Tuple<ModuleInfo, int>>(stringComparer);
            var modulesToLaunch = moduleCatalog.ToList();
            var remainingCount = int.MaxValue;
            while (modulesToLaunch.Count > 0)
            {
                if (remainingCount == modulesToLaunch.Count)
                {   //
                    // The remaining (unranked) modules count did not change for an iteration. This is a signal for a circular dependency.
                    //
                    throw new InvalidOperationException(
                        string.Format("A circular dependencies exists between some of the following modules: [{0}]", ", ".Join(modulesToLaunch.Select(x => x.Name))));
                }
                remainingCount = modulesToLaunch.Count;
                var modulesOfCurrentRank = new List<ModuleInfo>(modulesToLaunch.Count);
                foreach (var moduleInfo in modulesToLaunch)
                {
                    var rank = 0;
                    foreach (var moduleDependency in moduleInfo.RequiredModules)
                    {
                        Tuple<ModuleInfo, int> parentRank;
                        if (modulesWithRank.TryGetValue(moduleDependency.Name, out parentRank))
                        {
                            rank = Math.Max(rank, parentRank.Item2);
                        }
                        else
                        {   //
                            // The module has an unranked dependency, it cannot be ranked as well.
                            //
                            rank = -1;
                            break;
                        }
                    }
                    if (rank < 0)
                    {   //
                        // The module has depdendencies which are not ranked yet, meaning it must survive another iteration to determine rank.
                        //
                        continue;
                    }
                    //
                    // The module has no unranked dependencies, so its rank is determined as the the highest rank of its existing dependencies plus one.
                    // Modules with no dependencies will have a rank of 1.
                    //
                    modulesOfCurrentRank.Add(moduleInfo);
                    modulesWithRank[moduleInfo.Name] = Tuple.Create(moduleInfo, rank + 1);
                }
                modulesOfCurrentRank.ForEach(x => modulesToLaunch.Remove(x));
            }
            return modulesWithRank.Values.OrderBy(x => x.Item2).GroupBy(x => x.Item2, x => x.Item1);
        }
    }
}