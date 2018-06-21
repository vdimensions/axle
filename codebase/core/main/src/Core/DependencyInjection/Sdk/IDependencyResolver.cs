namespace Axle.Core.DependencyInjection.Sdk
{
    public interface IDependencyResolver
    {
        object Resolve(DependencyInfo dependency);
    }
}