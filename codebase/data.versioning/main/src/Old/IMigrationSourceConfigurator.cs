using Axle.Application.Modularity;
using Axle.Persistence.Migrations.Sdk;


namespace Axle.Persistence.Migrations
{
    [TransitiveModuleDependency(Application.PersistenceModule.Name)]
    public interface IMigrationSourceConfigurator : IAppConfigurator
    {
        void RegisterMigrationSources(IMigrationSourceRegistry registry);
    }
}
