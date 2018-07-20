#if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
using System;


namespace Axle.Modularity
{
    // ReSharper disable UnusedMember.Local
    [Module]
    [Requires(typeof(StatisticsModule))]
    internal sealed class DynamicModuleLoader : IAssemblyLoadListener
    {
        private readonly Application _application;

        public DynamicModuleLoader(Application application)
        {
            _application = application;
        }

        [ModuleInit]
        void Init() => DependencyInitialized(this);

        [ModuleTerminate]
        void Terminate() => DependencyTerminated(this);

        [ModuleDependencyInitialized]
        void DependencyInitialized(IAssemblyLoadListener listener)
        {
            AppDomain.CurrentDomain.AssemblyLoad += listener.OnAssemblyLoad;
        }

        [ModuleDependencyTerminated]
        void DependencyTerminated(IAssemblyLoadListener listener)
        {
            AppDomain.CurrentDomain.AssemblyLoad -= listener.OnAssemblyLoad;
        }

        public void OnAssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            _application.Execute(args.LoadedAssembly);
        }
    }
    // ReSharper restore UnusedMember.Local
}
#endif