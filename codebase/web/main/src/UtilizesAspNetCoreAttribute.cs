using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Web.AspNetCore
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class UtilizesAspNetCoreAttribute : UtilizesAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UtilizesAspNetCoreAttribute"/> class. 
        /// </summary>
        public UtilizesAspNetCoreAttribute() : base(typeof(AspNetCoreModule)) { }
    }
}