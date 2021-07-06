namespace Axle.DependencyInjection
{
    public interface IDependencyContainerFactory
    {
        IDependencyContainer CreateContainer();
        IDependencyContainer CreateContainer(IDependencyContext parent);
    }
}