using System.Diagnostics.CodeAnalysis;
using Axle.Data.Configuration;

namespace Axle.Data.DataSources
{
    public interface IDataSourceRegistry
    {
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        void RegisterDataSource(ConnectionStringInfo cs);
    }
}