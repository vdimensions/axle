using System;

using Axle.Modularity;


namespace Axle.Resources
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresResourcesAttribute : RequiresAttribute
    {
        public RequiresResourcesAttribute() : base(typeof(ResourcesModule))
        {
        }
    }
}