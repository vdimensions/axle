using System;
using Axle.Modularity;

namespace Axle.Web.AspNetCore
{
    /// <summary>
    /// An attribute which indicates that support for AspNetCore will be made available before the target module
    /// initializes.
    /// </summary>
    /// <seealso cref="UtilizesAspNetCoreAttribute"/>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresAspNetCoreAttribute : RequiresAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiresAspNetCoreAttribute"/> class. 
        /// </summary>
        public RequiresAspNetCoreAttribute() : base(typeof(AspNetCoreModule)) { }
    }
}