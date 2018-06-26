using Axle.Application.DependencyInjection;


namespace Axle.Application
{
    public interface IDependencyContainerProvider
    {
        IContainer Create(IContainer parent);
        IContainer Create();
    }
}