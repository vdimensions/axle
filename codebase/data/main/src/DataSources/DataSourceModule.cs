using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Axle.Application;
using Axle.Caching;
using Axle.Configuration;
using Axle.Data.Configuration;
using Axle.Data.DataSources.Resources.Extraction;
using Axle.DependencyInjection;
using Axle.Logging;
using Axle.Modularity;
using Axle.Resources;
using Axle.Resources.Bundling;
using Axle.Resources.Extraction;
using Axle.Verification;

namespace Axle.Data.DataSources
{
    
    [Module]
    [Requires(typeof(DataModule))]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal sealed class DataSourceModule : ISqlScriptLocationRegistry, IDataSourceRegistry
    {
        internal const string SqlScriptsBundle = "SqlScripts";

        private readonly DataModule _dataModule;
        private readonly DataSourceConfiguration _configuration;
        private readonly ResourceManager _dataSourceResourceManager;
        private readonly IResourceExtractor _scriptExtractor;
        private readonly IDictionary<string, DataSource> _dataSources = new ConcurrentDictionary<string, DataSource>(StringComparer.OrdinalIgnoreCase);

        public DataSourceModule(DataModule dataModule, IApplicationHost host, IConfiguration configuration, ILogger logger)
        {
            Logger = logger;
            _dataModule = dataModule;
            _configuration = new DataSourceConfiguration(configuration.GetConnectionStrings());
            _dataSourceResourceManager = host.CreateResourceManager(new SimpleCacheManager());
            _scriptExtractor = new SqlScriptSourceExtractor(Enumerable.Select(dataModule, x => x.DialectName));
        }

        [ModuleInit]
        internal void OnInit(IDependencyExporter exporter)
        {
            foreach (var cs in _configuration.ConnectionStrings)
            {
                RegisterDataSource(cs);
            }

            foreach (var dataSource in _dataSources.Values)
            {
                exporter.Export(dataSource, dataSource.Name);
            }
        }

        public void RegisterDataSource(ConnectionStringInfo cs)
        {
            var dataSources = _dataSources;
            if (!string.IsNullOrEmpty(cs.ProviderName))
            {
                var provider = _dataModule[cs.ProviderName];
                if (provider == null)
                {
                    Logger.Warn(
                        "No data source will be associated with connection string '{0}'. Provider '{1}' is not registered.",
                        cs.Name,
                        cs.ProviderName);
                }
                else
                {
                    var dataSource = new DataSource(cs.Name, provider, cs.ConnectionString, _dataSourceResourceManager);
                    Logger.Info(
                        "A data source was successfully created for connection string '{0}', using the following data provider: {1}.",
                        cs.Name,
                        cs.ProviderName);
                    dataSources.Add(dataSource.Name, dataSource);
                }
            }
            else
            {
                Logger.Warn(
                    "No data source could be associated with connection string '{0}'. The `{1}` field is not set.",
                    cs.Name,
                    nameof(cs.ProviderName));
            }
        }

        [ModuleDependencyInitialized]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        internal void OnSqlScriptBundleConfigurerInitialized(ISqlScriptLocationConfigurer configurer) => configurer.RegisterScriptLocations(this);

        [ModuleDependencyInitialized]
        internal void OnDataSourceProviderInitialized(IDataSourceProvider dataSourceProvider) => dataSourceProvider.RegisterDataSources(this);

        ISqlScriptLocationRegistry ISqlScriptLocationRegistry.Register(string bundle, Uri location)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(location, nameof(location)));
            PrepareBundle(bundle).Register(location);
            return this;
        }

        ISqlScriptLocationRegistry ISqlScriptLocationRegistry.Register(string bundle, Assembly assembly, string path)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(assembly, nameof(assembly)));
            StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(path, nameof(path)));
            ResourceBundleExtensions.Register(PrepareBundle(bundle), assembly, path);
            return this;
        }

        private IConfigurableBundleContent PrepareBundle(string bundle)
        {
            var b = _dataSourceResourceManager.Bundles.Configure(bundle);
            if (!Enumerable.Any(b.Extractors, e => ReferenceEquals(e, _scriptExtractor)))
            {
                ResourceExtractorRegistryExtensions.Register(b.Extractors, _scriptExtractor);
            }
            return b;
        }

        public ILogger Logger { get; }
    }
}
