using System;
using Axle.Modularity;

namespace Axle.Web.AspNetCore
{
    /// <summary>
    /// An attribute that, when applied on a module class, will establish the <see cref="AspNetCoreModule"/> as a
    /// dependency on the target module.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresAspNetCoreAttribute : RequiresAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiresAspNetCoreAttribute"/> class. 
        /// </summary>
        public RequiresAspNetCoreAttribute() : base(typeof(AspNetCoreModule)) { }
    }
}