namespace Axle.DependencyInjection
{
    public interface IDependencyContainerProvider
    {
        IContainer Create(IContainer parent);
        IContainer Create();
    }
}