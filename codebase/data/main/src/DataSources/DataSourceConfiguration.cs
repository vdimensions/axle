using System.Collections.Generic;
using Axle.Data.Configuration;

namespace Axle.Data.DataSources
{
    public sealed class DataSourceConfiguration
    {
        public IEnumerable<ConnectionStringInfo> ConnectionStrings { get; set; }
    }
}
