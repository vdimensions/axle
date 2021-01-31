using System;
using Axle.Modularity;

namespace Axle.Resources
{
    /// <summary>
    /// An attribute that indicates the target module has a dependency on the <see cref="ResourcesModule"/>. 
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