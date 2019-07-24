using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Axle.Configuration;
using Axle.Data.Configuration;
using Axle.Logging;
using Axle.Modularity;
using Axle.Resources;

namespace Axle.Data.DataSources
{
    [Module]
    [Requires(typeof(DataModule))]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal sealed class DataSourceModule
    {
        private readonly IDbServiceProviderRegistry _dbServiceProviders;
        private readonly IConfiguration _configuration;

        public DataSourceModule(IDbServiceProviderRegistry dbServiceProviderRegistry, IConfiguration configuration, ILogger logger)
        {
            Logger = logger;
            _dbServiceProviders = dbServiceProviderRegistry;
            _configuration = configuration;
        }

        [ModuleInit]
        internal void OnInit(ModuleExporter exporter)
        {
            var dataSources = new Dictionary<string, DataSource>(StringComparer.OrdinalIgnoreCase);
            #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
            foreach (var cs in _configuration.GetConnectionStrings())
            {
                if (!string.IsNullOrEmpty(cs.ProviderName))
                {
                    var provider = _dbServiceProviders[cs.ProviderName];
                    if (provider == null)
                    {
                        Logger.Warn(
                            "No data source will be associated with connection string '{0}'. Provider '{1}' is not registered.",
                            cs.Name,
                            cs.ProviderName);
                    }
                    else
                    {
                        var dataSource = new DataSource(cs.Name, provider, cs.ConnectionString, new DefaultResourceManager());
                        Logger.Trace(
                            "A data source was successfully created for connection string '{0}', using the following data provider: {1}.",
                            cs.Name,
                            cs.ProviderName);
                        dataSources.Add(dataSource.Name, dataSource);
                    }
                }
                else
                {
                    Logger.Warn(
                        "No data source will be associated with connection string '{0}'. The `{1}` field is not set.",
                        cs.Name,
                        nameof(cs.ProviderName));
                }
            }
            #endif

            foreach (var dataSource in dataSources.Values)
            {
                exporter.Export(dataSource, dataSource.Name);
            }
        }

        public ILogger Logger { get; }
    }
}
