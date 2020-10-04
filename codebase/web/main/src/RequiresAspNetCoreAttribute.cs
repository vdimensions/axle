using System;
using Axle.Modularity;

namespace Axle.Web.AspNetCore
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresAspNetCoreAttribute : RequiresAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiresAspNetCoreAttribute"/> class. 
        /// </summary>
        public RequiresAspNetCoreAttribute() : base(typeof(AspNetCoreModule)) { }
    }
}