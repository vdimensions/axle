using System;
using System.Collections;
using System.Collections.Generic;
using Axle.Application.Services;
using Axle.DependencyInjection;
using Axle.Modularity;

namespace Axle.Data
{
    [Module]
    [Requires(typeof(ServiceRegistry))]
    internal sealed class DataModule : ServiceGroup<DataModule, IDbServiceProvider>, IEnumerable<IDbServiceProvider>
    {
        private readonly IDictionary<string, IDbServiceProvider> _providers = new Dictionary<string, IDbServiceProvider>(StringComparer.Ordinal);
        
        public DataModule(ServiceRegistry serviceRegistry) : base(serviceRegistry)
        {
            foreach (var providerModule in serviceRegistry)
            {
                if (_providers.ContainsKey(providerModule.ProviderName))
                {
                    throw new InvalidOperationException($"A database service provider '{providerModule.ProviderName}' is already registered!");
                }
                else 
                {
                    _providers.Add(providerModule.ProviderName, providerModule);
                }
            }
        }

        protected override void Initialize(IDependencyExporter exporter) => exporter.Export(this);

        IEnumerator<IDbServiceProvider> IEnumerable<IDbServiceProvider>.GetEnumerator() => _providers.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _providers.Values.GetEnumerator();
        
        public IDbServiceProvider this[string name] => _providers.TryGetValue(name, out var provider) ? provider : null;
    }
}