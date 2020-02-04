using System.Collections.Generic;

namespace Axle.Data.Configuration
{
    public sealed class DataSourceConfiguration
    {
        public DataSourceConfiguration(IEnumerable<ConnectionStringInfo> connectionStrings)
        {
            ConnectionStrings = connectionStrings;
        }

        public IEnumerable<ConnectionStringInfo> ConnectionStrings { get; }
    }
}
