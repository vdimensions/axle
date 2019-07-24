using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Axle.Data.Resources.Extraction;
using Axle.Modularity;
using Axle.Resources;
using Axle.Resources.Bundling;
using Axle.Verification;

namespace Axle.Data.Resources
{
    [Module]
    [Requires(typeof(DbServiceProviderRegistry))]
    internal sealed class SqlScriptSourceModule : IResourceBundleConfigurer, ISqlScriptLocationRegistry
    {
        public const string Bundle = "SqlScripts";

        private IConfigurableBundleContent _bundle;

        public void Configure(IResourceBundleRegistry registry)
        {
            _bundle = registry.Configure(Bundle);
            _bundle.Extractors.Register(new SqlScriptSourceExtractor(Enumerable.Select(Registry, x => x.DialectName)));
        }

        [ModuleDependencyInitialized]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        internal void OnSqlScriptBundleConfigurerInitialized(ISqlScriptLocationConfigurer configurer) => configurer.RegisterScriptLocations(this);

        public ISqlScriptLocationRegistry Register(Uri location)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(location, nameof(location)));
            _bundle.Register(location);
            return this;
        }
        public ISqlScriptLocationRegistry Register(Assembly assembly, string path)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(assembly, nameof(assembly)));
            StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(path, nameof(path)));
            ResourceBundleExtensions.Register(_bundle, assembly, path);
            return this;
        }

        internal IDbServiceProviderRegistry Registry { get; set; }
    }
}
