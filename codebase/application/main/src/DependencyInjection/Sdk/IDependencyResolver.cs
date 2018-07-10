namespace Axle.DependencyInjection.Sdk
{
    public interface IDependencyResolver
    {
        object Resolve(DependencyInfo dependency);
    }
}