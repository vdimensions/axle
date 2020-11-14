using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Axle.DependencyInjection;
using Axle.Modularity;

namespace Axle.Application.Services
{
    [Module]
    [Requires(typeof(ServiceCollector))]
    internal sealed class ServiceRegistry
    {
        [Module]
        internal sealed class ServiceCollector : IEnumerable<ServiceInfo>
        {
            private readonly ConcurrentQueue<ServiceInfo> _services = new ConcurrentQueue<ServiceInfo>();
        
            [ModuleDependencyInitialized(AllowParallelInvoke = true)]
            [SuppressMessage("ReSharper", "UnusedMember.Global")]
            internal void OnServiceInitialized(object maybeService)
            {
                if (ServiceInfo.TryGetServiceInfo(maybeService, out var serviceInfo))
                {
                    _services.Enqueue(serviceInfo);
                }
            }

            private IEnumerator<ServiceInfo> GetEnumerator() => _services.GetEnumerator();
            IEnumerator<ServiceInfo> IEnumerable<ServiceInfo>.GetEnumerator() => GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
        
        private readonly ServiceCollector _serviceCollector;

        public ServiceRegistry(ServiceCollector serviceCollector)
        {
            _serviceCollector = serviceCollector;
        }
        
        [ModuleInit]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        internal void Initialize(IDependencyExporter exporter)
        {
            foreach (var service in _serviceCollector)
            {
                exporter.Export(service.Instance, service.Name ?? string.Empty);
            }
        }
    }
}
