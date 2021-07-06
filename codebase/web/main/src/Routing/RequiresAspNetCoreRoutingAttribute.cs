using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Web.AspNetCore.Routing
{
    /// <summary>
    /// An attribute that, when applied on a module class, will establish the <see cref="AspNetCoreRoutingModule"/> as a
    /// dependency on the target module.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresAspNetCoreRoutingAttribute : RequiresAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiresAspNetCoreRoutingAttribute"/> class. 
        /// </summary>
        public RequiresAspNetCoreRoutingAttribute() : base(typeof(AspNetCoreRoutingModule)) { }
    }
}