using System.Diagnostics.CodeAnalysis;
#if !(NETSTANDARD || NET40_OR_NEWER) 
using System.Linq;
#endif
using Axle.Data.DataSources;
using Axle.Data.Versioning.Changeset;
using Axle.Data.Versioning.Configuration;
using Axle.Modularity;

namespace Axle.Data.Versioning.Migrations
{
    [Module]
    [ModuleConfigSection(typeof(DbVersioningConfig), "axle.data.versioning")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal sealed class MigratorScriptProviderModule : ISqlScriptLocationConfigurer, _DbChangesetConfigurer
    {
        public static readonly string Bundle = nameof(DbChangelog);

        private readonly DbVersioningConfig _config;

        public MigratorScriptProviderModule(DbVersioningConfig config)
        {
            _config = config;
        }
        public MigratorScriptProviderModule() : this(null) { }

        void ISqlScriptLocationConfigurer.RegisterScriptLocations(ISqlScriptLocationRegistry registry)
        {
            registry.Register(Bundle, GetType().Assembly, $"Properties/Queries/{Bundle}/");
        }
        
        public void Configure(IDbChangesetRegistry registry)
        {
            if (_config?.Migrations != null)
            {
                #if NETSTANDARD || NET40_OR_NEWER 
                registry.Register(_config.Migrations);
                #else
                registry.Register(_config.Migrations.Cast<IDbChangeset>());
                #endif
            }
        }
    }
}