using Axle.Core.DependencyInjection;


namespace Axle.Core
{
    public interface IDependencyContainerProvider
    {
        IContainer Create(IContainer parent);
        IContainer Create();
    }
}