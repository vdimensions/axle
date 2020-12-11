using Axle.Modularity;

namespace Axle.Data.MySql
{
    [Module]
    [DbServiceProvider(Name = MySqlServiceProvider.Name)]
    internal sealed class MySqlModule : DbServiceProvider
    {
        public MySqlModule() : base(MySqlServiceProvider.Instance) { }
    }
}