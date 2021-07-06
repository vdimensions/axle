using System;
using System.Collections.Generic;
using System.Linq;
using Axle.Application;
using Axle.Data.DataSources;
using Axle.Data.Versioning.Changeset;
using Axle.Data.Versioning.Configuration;
using Axle.DependencyInjection;
using Axle.Modularity;

namespace Axle.Data.Versioning.Migrations
{
    [Module]
    [Requires(typeof(DbChangesetModule))]
    [Requires(typeof(MigratorScriptProviderModule))]
    [ModuleConfigSection(typeof(DbVersioningConfig), "axle.data.versioning")]
    [RequiresDataSources]
    internal sealed class MigratorModule
    {
        private readonly IDependencyContext _container;
        private readonly IApplicationHost _host;
        private readonly DbChangesetModule _dbChangesetModule;

        public MigratorModule(
            DbChangesetModule dbChangesetModule,
            IDependencyContext container,
            IApplicationHost host)
        {
            _container = container;
            _host = host;
            _dbChangesetModule = dbChangesetModule;
        }

        [ModuleInit]
        internal void Init()
        {
            var dataSourcesByName = new Dictionary<string, IDataSource>(StringComparer.Ordinal);
            var migrationEnginesByType = new Dictionary<Type, IMigrationEngine>();
            
            foreach (var changelog in _dbChangesetModule.OrderBy(x => x.ScriptResource.Name))
            {
                var dataSourceName = changelog.DataSourceName;
                if (!dataSourcesByName.TryGetValue(dataSourceName, out var dataSource))
                {
                    if (_container.TryResolve(out dataSource, dataSourceName) || _container.Parent.TryResolve(out dataSource, dataSourceName))
                    {
                        dataSourcesByName[dataSourceName] = dataSource;
                    }
                    else
                    {
                        throw new InvalidOperationException($"Unable to resolve datasource '{dataSourceName}. ");
                    }
                }
                
                var migrationEngineType = changelog.MigrationEngineType ?? typeof(AxleMigrationEngine);
                if (!migrationEnginesByType.TryGetValue(migrationEngineType, out var migrationEngine))
                {
                    using (var tmpContainer = _host.DependencyContainerFactory.CreateContainer(_container))
                    {
                        tmpContainer.Export(_host.LoggingService.CreateLogger(migrationEngineType));
                        tmpContainer.RegisterType(migrationEngineType);
                        migrationEnginesByType[migrationEngineType] = migrationEngine = tmpContainer.Resolve<IMigrationEngine>();
                    }
                }

                var res = changelog.ScriptResource;
                using (var stream = res.Open())
                {
                    migrationEngine.Migrate(dataSource, res.Name, stream); 
                }
            }
        }
    }
}