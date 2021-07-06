using System;
using Axle.Modularity;

namespace Axle.Resources
{
    /// <summary>
    /// An attribute which indicates that support for resources will be made available before the target module
    /// initializes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresResourcesAttribute : RequiresAttribute
    {
        /// <summary>
        /// Creates a new instance of the <see cref="RequiresResourcesAttribute"/> class.
        /// </summary>
        public RequiresResourcesAttribute() : base(typeof(ResourcesModule)) { }
    }
}