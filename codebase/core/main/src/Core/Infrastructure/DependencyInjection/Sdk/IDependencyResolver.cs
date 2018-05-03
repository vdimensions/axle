namespace Axle.Core.Infrastructure.DependencyInjection.Sdk
{
    public interface IDependencyResolver
    {
        object Resolve(DependencyInfo dependency);
    }
}