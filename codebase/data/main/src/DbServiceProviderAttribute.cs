using Axle.Application.Services;
using Axle.Modularity;

namespace Axle.Data
{
    /// <summary>
    /// An attribute to apply ono a module that should act as a <see cref="IDbServiceProvider"/>.
    /// </summary>
    /// <seealso cref="DbServiceProvider"/>
    [Requires(typeof(DataModule.ServiceRegistry))]
    [ProvidesFor(typeof(DataModule))]
    public sealed class DbServiceProviderAttribute : AbstractServiceAttribute { }
}