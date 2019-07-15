using System.Linq;
using Axle.Data.Resources.Extraction;
using Axle.Modularity;
using Axle.Resources;
using Axle.Resources.Bundling;

namespace Axle.Data.Resources
{
    [Module]
    [Requires(typeof(DbServiceProviderRegistry))]
    internal sealed class SqlScriptSourceModule : IResourceBundleConfigurer
    {
        public const string Bundle = "SqlScripts";

        public void Configure(IResourceBundleRegistry registry)
        {
            registry.Configure(Bundle).Extractors.Register(new SqlScriptSourceExtractor(Registry.Select(x => x.DialectName)));
        }

        internal IDbServiceProviderRegistry Registry { get; set; }
    }
}
