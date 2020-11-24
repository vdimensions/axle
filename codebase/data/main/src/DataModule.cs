using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Axle.Application.Services;
using Axle.DependencyInjection;
using Axle.Modularity;

namespace Axle.Data
{
    [Module]
    [Requires(typeof(ServiceRegistry))]
    internal sealed class DataModule : ServiceGroup<DataModule, DatabaseServiceProviderModule>, IEnumerable<IDbServiceProvider>
    {
        private readonly ConcurrentDictionary<string, IDbServiceProvider> _providers = new ConcurrentDictionary<string, IDbServiceProvider>(StringComparer.Ordinal);
        
        public DataModule(ServiceRegistry serviceRegistry) : base(serviceRegistry)
        {
            foreach (var providerModule in serviceRegistry)
            {
                if (!_providers.TryAdd(providerModule.Provider.ProviderName, providerModule.Provider))
                {
                    throw new InvalidOperationException($"A database service provider '{providerModule.Provider.ProviderName}' is already registered!");
                }
            }
        }

        protected override void Initialize(IDependencyExporter exporter) => exporter.Export(this);

        IEnumerator<IDbServiceProvider> IEnumerable<IDbServiceProvider>.GetEnumerator() => _providers.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _providers.Values.GetEnumerator();
        
        public IDbServiceProvider this[string name] => _providers.TryGetValue(name, out var provider) ? provider : null;
    }
}