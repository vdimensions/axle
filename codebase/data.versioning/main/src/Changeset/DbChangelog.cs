using System;
using Axle.Resources;

namespace Axle.Data.Versioning.Changeset
{
    public sealed class DbChangelog
    {
        public DbChangelog(string dataSourceName, ResourceInfo scriptResource, Type migrationEngineType)
        {
            DataSourceName = dataSourceName;
            ScriptResource = scriptResource;
            MigrationEngineType = migrationEngineType;
        }

        public string DataSourceName { get; }
        public ResourceInfo ScriptResource { get; }
        public Type MigrationEngineType { get; }
    }
}