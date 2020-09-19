using System.Linq;
using Axle.Data.DataSources;
using Axle.Data.Versioning.Changeset;
using Axle.Data.Versioning.Configuration;
using Axle.Modularity;

namespace Axle.Data.Versioning.Migrations
{
    [Module]
    [ModuleConfigSection(typeof(DbVersioningConfig), "axle.data.versioning")]
    internal sealed class MigratorDbScriptsModule : ISqlScriptLocationConfigurer, _DbChangesetConfigurer
    {
        public static readonly string Bundle = nameof(DbChangelog);

        private readonly DbVersioningConfig _config;

        public MigratorDbScriptsModule(DbVersioningConfig config)
        {
            _config = config;
        }

        void ISqlScriptLocationConfigurer.RegisterScriptLocations(ISqlScriptLocationRegistry registry)
        {
            registry.Register(Bundle, GetType().Assembly, $"Properties/Queries/{Bundle}/");
        }
        
        public void Configure(IDbChangesetRegistry registry)
        {
            if (_config?.Migrations != null)
            {
                registry.Register(_config.Migrations.Cast<IDbChangeset>());
            }
        }
    }
}