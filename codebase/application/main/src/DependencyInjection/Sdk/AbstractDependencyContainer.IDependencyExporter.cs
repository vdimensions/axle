#if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
namespace Axle.DependencyInjection.Sdk
{
    partial class AbstractDependencyContainer : IDependencyExporter
    {
        IDependencyExporter IDependencyExporter.Export(object instance, string name)
        {
            return RegisterInstance(instance, name);
        }
    }
}
#endif