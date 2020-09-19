using System;
using System.Collections.Generic;
using System.Linq;
using Axle.Data.DataSources;
using Axle.Data.Versioning.Changeset;
using Axle.Data.Versioning.Configuration;
using Axle.DependencyInjection;
using Axle.Modularity;

namespace Axle.Data.Versioning.Migrations
{
    [Module]
    [Requires(typeof(DbChangesetModule))]
    [Requires(typeof(MigratorDbScriptsModule))]
    [ModuleConfigSection(typeof(DbVersioningConfig), "axle.data.versioning")]
    internal sealed class MigratorModule : IDbChangesetConfigurer
    {
        private readonly DbVersioningConfig _config;
        private readonly IDependencyContainer _container;
        private readonly IApplicationHost _host;
        private readonly DbChangesetModule _dbChangesetModule;

        public MigratorModule(
            DbChangesetModule dbChangesetModule,
            IDependencyContainer container,
            IApplicationHost host, 
            DbVersioningConfig config)
        {
            _config = config;
            _container = container;
            _host = host;
            _dbChangesetModule = dbChangesetModule;
        }
        public MigratorModule(
                DbChangesetModule dbChangesetModule,
                IDependencyContainer container,
                IApplicationHost host) 
            : this(dbChangesetModule, container, host, null) { }

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
                    dataSourcesByName[dataSourceName] = dataSource = _container.Resolve<IDataSource>(dataSourceName);
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

        void IDbChangesetConfigurer.Configure(IDbChangesetRegistry registry)
        {
            if (_config?.Migrations != null)
            {
                registry.Register(_config.Migrations);
            }
        }
    }
}