using System;

using Axle.Modularity;


namespace Axle.Resources
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresResourcesAttribute : RequiresAttribute
    {
        /// <summary>
        /// Creates a new instance of the <see cref="RequiresResourcesAttribute"/> class.
        /// </summary>
        public RequiresResourcesAttribute() : base(typeof(ResourcesModule))
        {
        }
    }
}