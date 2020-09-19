using Axle.Data.Versioning.Migrations;
using Axle.Modularity;

namespace Axle.Data.Versioning.Changeset
{
    [Module]
    [Requires(typeof(DbChangesetModule))]
    [ProvidesFor(typeof(MigratorModule))]
    public interface IDbChangesetConfigurer
    {
        void Configure(IDbChangesetRegistry dbChangesetRegistry);
    }
}