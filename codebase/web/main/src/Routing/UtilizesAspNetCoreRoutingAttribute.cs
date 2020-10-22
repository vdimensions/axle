using System;
using System.Diagnostics.CodeAnalysis;

using Axle.Modularity;


namespace Axle.Web.AspNetCore.Routing
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class UtilizesAspNetCoreRoutingAttribute : UtilizesAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UtilizesAspNetCoreRoutingAttribute"/> class. 
        /// </summary>
        public UtilizesAspNetCoreRoutingAttribute() : base(typeof(AspNetCoreRoutingModule)) { }
    }
}