using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Application.Services
{
    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public abstract class AbstractServiceAttribute : Attribute
    {
        public string Name { get; set; }

        /// <summary>
        /// A <see cref="bool"/> value determining whether the service should be registered to the root
        /// dependency container and therefore accessible across the entire application.
        /// The default value is <c>true</c>
        /// </summary>
        public bool IsPublic { get; set; } = true;
    }
    
    [Requires(typeof(ServiceGroup.ServiceRegistry))]
    [ProvidesFor(typeof(ServiceGroup))]
    public sealed class ServiceAttribute : AbstractServiceAttribute
    {
    }
    // TODO: uncomment when C# starts supporting generic attributes
    // [Requires(typeof(ServiceGroup<T>.ServiceRegistry))]
    // [ProvidesFor(typeof(ServiceGroup<T>))]
    // public sealed class ServiceAttribute<T> : AbstractServiceAttribute
    // {
    // }
}