using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Axle.Modularity;


namespace Axle.Data
{
    [Module]
    internal sealed class DbServiceProviderRegistry : IEnumerable<IDbServiceProvider>
    {
        private readonly ConcurrentDictionary<string, IDbServiceProvider> _providers = new ConcurrentDictionary<string, IDbServiceProvider>(StringComparer.Ordinal);

        [ModuleInit]
        internal void Init(ModuleExporter exporter)
        {
            exporter.Export(this);
        }

        [ModuleDependencyInitialized]
        internal void OnDbServiceProviderInitialized(DatabaseServiceProviderModule module)
        {
            var provider = module.Provider;
            _providers.AddOrUpdate(provider.ProviderName, provider, (_, __) => provider);
        }

        [ModuleDependencyTerminated]
        internal void OnDbServiceProviderTerminated(DatabaseServiceProviderModule module)
        {
            var provider = module.Provider;
            _providers.TryRemove(provider.ProviderName, out _);
        }

        IEnumerator<IDbServiceProvider> IEnumerable<IDbServiceProvider>.GetEnumerator() => _providers.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _providers.Values.GetEnumerator();
    }
}
