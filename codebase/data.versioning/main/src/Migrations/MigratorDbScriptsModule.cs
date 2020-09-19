using Axle.Data.DataSources;
using Axle.Data.Versioning.Changeset;
using Axle.Modularity;

namespace Axle.Data.Versioning.Migrations
{
    [Module]
    internal sealed class MigratorDbScriptsModule : ISqlScriptLocationConfigurer
    {
        public static readonly string Bundle = nameof(DbChangelog);
        
        void ISqlScriptLocationConfigurer.RegisterScriptLocations(ISqlScriptLocationRegistry registry)
        {
            registry.Register(Bundle, GetType().Assembly, $"Properties/Queries/{Bundle}/");
        }
    }
}