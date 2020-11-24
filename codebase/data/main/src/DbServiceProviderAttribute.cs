using Axle.Application.Services;
using Axle.Modularity;

namespace Axle.Data
{
    [Requires(typeof(DataModule.ServiceRegistry))]
    [ProvidesFor(typeof(DataModule))]
    public sealed class DbServiceProviderAttribute : AbstractServiceAttribute { }
}