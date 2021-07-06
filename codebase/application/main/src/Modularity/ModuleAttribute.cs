using System;
using Axle.Application;


namespace Axle.Modularity
{
    /// <summary>
    /// An attribute that marks a class as an application module.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class ModuleAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the type of the <see cref="IApplicationHost"/> capable of working with the annotated module.
        /// The default value is <c>null</c>, which allows the module to be hosted by any <see cref="IApplicationHost"/>
        /// implementation.
        /// </summary>
        public Type HostableBy { get; set; }
    }
}