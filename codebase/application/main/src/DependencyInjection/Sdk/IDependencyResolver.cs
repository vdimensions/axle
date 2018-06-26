namespace Axle.Application.DependencyInjection.Sdk
{
    public interface IDependencyResolver
    {
        object Resolve(DependencyInfo dependency);
    }
}