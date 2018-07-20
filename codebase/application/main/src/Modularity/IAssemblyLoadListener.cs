#if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
using System;


namespace Axle.Modularity
{
    [Module]
    [Requires(typeof(DynamicModuleLoader))]
    public interface IAssemblyLoadListener
    {
        void OnAssemblyLoad(object sender, AssemblyLoadEventArgs args);
    }
}
#endif