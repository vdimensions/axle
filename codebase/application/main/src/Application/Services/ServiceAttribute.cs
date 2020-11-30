using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Application.Services
{
    /// <summary>
    /// An <see langword="abstract"/> class to be used for implementing a service attribute.
    /// A service attribute is reposnsible for establishing the proper module initialization order and must be unique
    /// for each service group implementation.
    /// You may also refer to the <see cref="ServiceAttribute"/> which is the service attribute implementation for the 
    /// default service group.
    /// </summary>
    /// <seealso cref="ServiceAttribute"/>
    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public abstract class AbstractServiceAttribute : Attribute
    {
        /// <summary>
        /// Gets the name of the service. This name, if provided, will be used as an alias for the 
        /// dependency that will be registered within the root dependency container.
        /// </summary>
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