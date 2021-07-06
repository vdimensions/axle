using Axle.Modularity;

namespace Axle.Application.Services
{
    [Requires(typeof(ServiceGroup.ServiceRegistry))]
    [ProvidesFor(typeof(ServiceGroup))]
    public sealed class ServiceAttribute : AbstractServiceAttribute
    {
    }
    // TODO: uncomment when C# starts supporting generic attributes
    // [Requires(typeof(ServiceGroup<T>.ServiceRegistry))]
    // [ProvidesFor(typeof(ServiceGroup<T>))]
    // public sealed class ServiceAttribute<T> : AbstractServiceAttribute
    // {
    // }
}