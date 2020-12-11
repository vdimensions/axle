using Axle.Modularity;

namespace Axle.Data.MySql
{
    [Module]
    [DbServiceProvider(Name = MariaDbServiceProvider.Name)]
    internal sealed class MariaDbModule : DbServiceProvider
    {
        public MariaDbModule() : base(MariaDbServiceProvider.Instance) { }
    }
}