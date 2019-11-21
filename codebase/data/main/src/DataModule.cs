using Axle.Modularity;

namespace Axle.Data
{
    /// <summary>
    /// The role of this module is to get initialized once
    /// the available datasources and db service provider objects for an application
    /// are fully discovered an usable.
    /// 
    /// The datasources module has a dependency on this module in order to ensure
    /// the availability of all db service providers, thus valid datasources can be
    /// instantiated.
    /// </summary>
    [Module]
    [Requires(typeof(DbServiceProviderRegistry))]
    internal sealed class DataModule { }
}