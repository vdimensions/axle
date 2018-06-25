using System;
using System.Collections.Generic;


namespace Axle.Core.Modularity
{
    public interface IModuleInfo
    {
        string ModuleName { get; }
        Type ModuleType { get; }
        string ModuleVersion { get; }
        bool IsEagerLoaded { get; }

        IEnumerable<IModuleDependency> RequiredModules { get; }
    }

    public class ModuleInfo
    {
        public string Name { get; }
        public Type Type { get; }

    }
}