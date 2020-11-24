using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Axle.DependencyInjection;
using Axle.Modularity;

namespace Axle.Application.Services
{
    /// <summary>
    /// An abstract class for a service group. A service group is a special type of
    /// module which consists of a local service registry (which is a dedicated module for collecting dependent
    /// services) and a the service group itself, which will take as a dependency all the services registered with the
    /// service registry. 
    /// </summary>
    /// <typeparam name="T">
    /// A generic type to uniquely identify a service group. 
    /// Usually this is the concrete implementation of the particular service group.
    /// </typeparam>
    /// <typeparam name="TService">
    /// The type of the services belonging to this service group.
    /// <para>
    /// This parameter can be used also as a type constraint to prevent non-conforming modules to be identified as
    /// members to the represented service group.
    /// To disable the constraint effect, use <see cref="object"/> in place of <typeparamref name="TService"/>.
    /// </para>
    /// </typeparam>
    [Module]
    public abstract class ServiceGroup<T, TService>
    {
        /// <summary>
        /// The service registry local to the encompassing service group implementation.
        /// Acts as a collector for modules expressing their belonging to the service group. 
        /// </summary>
        [Module]
        public sealed class ServiceRegistry : IEnumerable<TService>
        {
            private readonly ConcurrentQueue<ServiceInfo<TService>> _services = new ConcurrentQueue<ServiceInfo<TService>>();
        
            [ModuleDependencyInitialized(AllowParallelInvoke = true)]
            [SuppressMessage("ReSharper", "UnusedMember.Global")]
            internal void OnServiceInitialized(object maybeService)
            {
                if (ServiceInfo<TService>.TryGetServiceInfo(maybeService, out var serviceInfo))
                {
                    _services.Enqueue(serviceInfo);
                }
            }

            private IEnumerator<TService> GetEnumerator() => Services.Select(x => x.Instance).GetEnumerator();
            IEnumerator<TService> IEnumerable<TService>.GetEnumerator() => GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            internal IEnumerable<ServiceInfo<TService>> Services => _services.ToArray();
        }
        
        private readonly ServiceRegistry _serviceRegistry;

        /// <summary>
        /// Initializes a new instance of the current <see cref="ServiceGroup{T, TService}"/> implementation.
        /// </summary>
        /// <param name="serviceRegistry">
        /// The <see cref="ServiceRegistry"/> instance to register dependent services with.
        /// </param>
        protected ServiceGroup(ServiceRegistry serviceRegistry)
        {
            _serviceRegistry = serviceRegistry;
        }
        
        [ModuleInit]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        internal void InitializeServices(IDependencyExporter exporter)
        {
            foreach (var service in _serviceRegistry.Services.Where(service => service.IsPublic))
            {
                exporter.Export(service.Instance, service.Name ?? string.Empty);
            }

            Initialize(exporter);
        }
        
        protected virtual void Initialize(IDependencyExporter exporter) { }
    }
    
    /// <summary>
    /// A ready to use service group implementation. Annotate services with the <see cref="ServiceAttribute"/> to make
    /// them initialize before the <see cref="ServiceGroup"/> module.
    /// </summary>
    [Module]
    [Requires(typeof(ServiceRegistry))]
    public sealed class ServiceGroup : ServiceGroup<ServiceGroup, object>
    {
        internal ServiceGroup(ServiceRegistry serviceRegistry) : base(serviceRegistry) { }
    }
}
