using System.IO;
using Axle.Data.DataSources;

namespace Axle.Data.Versioning.Migrations
{
    public interface IMigrationEngine
    {
        void Migrate(IDataSource dataSource, string migrationName, Stream migrationStream);
    }
}