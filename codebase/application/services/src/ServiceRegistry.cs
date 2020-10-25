using System.Diagnostics.CodeAnalysis;
using Axle.DependencyInjection;
using Axle.Modularity;

namespace Axle.Application.Services
{
    [Module]
    [Requires(typeof(ServiceCollector))]
    internal sealed class ServiceRegistry
    {
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
