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
        private readonly IDictionary<string, IDbServiceProvider> _providers = new Dictionary<string, IDbServiceProvider>(StringComparer.Ordinal);
        
        public DataModule(ServiceRegistry serviceRegistry) : base(serviceRegistry)
        {
            foreach (var providerModule in serviceRegistry)
            {
                if (_providers.ContainsKey(providerModule.Provider.ProviderName))
                {
                    throw new InvalidOperationException($"A database service provider '{providerModule.Provider.ProviderName}' is already registered!");
                }
                else 
                {
                    _providers.Add(providerModule.Provider.ProviderName, providerModule.Provider);
                }
            }
        }

        protected override void Initialize(IDependencyExporter exporter) => exporter.Export(this);

        IEnumerator<IDbServiceProvider> IEnumerable<IDbServiceProvider>.GetEnumerator() => _providers.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _providers.Values.GetEnumerator();
        
        public IDbServiceProvider this[string name] => _providers.TryGetValue(name, out var provider) ? provider : null;
    }
}