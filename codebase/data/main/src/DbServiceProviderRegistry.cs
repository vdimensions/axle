using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Axle.DependencyInjection;
using Axle.Modularity;
using Axle.Verification;

namespace Axle.Data
{
    [Module]
    internal sealed class DbServiceProviderRegistry : IDbServiceProviderRegistry
    {
        private readonly ConcurrentDictionary<string, IDbServiceProvider> _providers = new ConcurrentDictionary<string, IDbServiceProvider>(StringComparer.Ordinal);
        
        [ModuleInit]
        internal void Init(IDependencyExporter exporter)
        {
            exporter.Export(this);
        }
        
        [ModuleDependencyInitialized]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        internal void OnDbServiceProviderInitialized(DatabaseServiceProviderModule module)
        {
            var provider = module.Provider;
            _providers.AddOrUpdate(provider.ProviderName, provider, (_, __) => provider);
        }

        [ModuleDependencyTerminated]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        internal void OnDbServiceProviderTerminated(DatabaseServiceProviderModule module)
        {
            var provider = module.Provider;
            _providers.TryRemove(provider.ProviderName, out _);
        }

        IEnumerator<IDbServiceProvider> IEnumerable<IDbServiceProvider>.GetEnumerator() => _providers.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _providers.Values.GetEnumerator();

        public IDbServiceProvider this[string name]
        {
            get
            {
                name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();
                return _providers.TryGetValue(name, out var res) ? res : null;
            }
        }
    }
}
