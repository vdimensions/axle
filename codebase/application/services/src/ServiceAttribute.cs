using System;
using Axle.Modularity;

namespace Axle.Application.Services
{
    [Requires(typeof(ServiceRegistry.ServiceCollector))]
    [ProvidesFor(typeof(ServiceRegistry))]
    public sealed class ServiceAttribute : Attribute
    {
        public string Name { get; set; }
    }
}