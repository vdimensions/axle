#if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
namespace Axle.DependencyInjection.Sdk
{
    partial class AbstractDependencyContainer : IDependencyExporter
    {
        IDependencyExporter IDependencyExporter.Export(object instance, string name)
        {
            return this.RegisterInstance(instance, name);
        }
    }
}
#endif