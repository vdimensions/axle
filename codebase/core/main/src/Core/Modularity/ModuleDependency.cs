namespace Axle.Core.Modularity
{
    internal sealed class ModuleDependency : IModuleDependency
    {
        public ModuleDependency(string moduleName, bool notifyOnInit, bool shareDependencies)
        {
            DependencyName = moduleName;
            NotifyOnInit = notifyOnInit;
            ShareDependencies = shareDependencies;
        }

        public string DependencyName { get; private set; }
        public bool NotifyOnInit { get; private set; }
        public bool ShareDependencies { get; private set; }
    }
}