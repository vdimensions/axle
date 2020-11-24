using Axle.Modularity;

namespace Axle.Application.Services
{
    [Requires(typeof(ServiceGroup))]
    public interface IServiceClient { }
    
    // TODO: uncomment when C# supports generic attributes
    // [Requires(typeof(ServiceGroup<T>))]
    // public interface IServiceClient<T> { }
}