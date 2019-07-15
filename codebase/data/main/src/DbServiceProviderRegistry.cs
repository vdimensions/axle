using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Axle.Modularity;
using Axle.Verification;


namespace Axle.Data
{
    internal interface IDbServiceProviderRegistry : IEnumerable<IDbServiceProvider>
    {
        IDbServiceProvider this[string providerName] { get; }
    }
    [Module]
    internal sealed class DbServiceProviderRegistry : IDbServiceProviderRegistry
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

        IDbServiceProvider IDbServiceProviderRegistry.this[string name]
        {
            get => _providers.TryGetValue(name.VerifyArgument(nameof(name)), out var res) ? res : null;
        }
    }
}
