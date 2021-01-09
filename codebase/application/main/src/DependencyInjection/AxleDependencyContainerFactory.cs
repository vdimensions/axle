#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
namespace Axle.DependencyInjection
{
    public sealed class AxleDependencyContainerFactory : IDependencyContainerFactory
    {
        IDependencyContainer IDependencyContainerFactory.CreateContainer(IDependencyContext parent) => new DependencyContainer(parent);

        IDependencyContainer IDependencyContainerFactory.CreateContainer() => new DependencyContainer(null);
    }
}
#endif