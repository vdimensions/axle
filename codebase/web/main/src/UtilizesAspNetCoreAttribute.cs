using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Web.AspNetCore
{
    /// <summary>
    /// If the application uses AspNetCore, this attribute will cause that support for AspNetCore will be made available
    /// before the target module initializes, similarly to the <see cref="RequiresAspNetCoreAttribute"/> works.
    /// However, unlike the <see cref="RequiresAspNetCoreAttribute"/>, this attribute will not trigger the
    /// initialization of AspNetCore, making it an optional dependency. If AspNetCore is not requested by another module
    /// or trough application config, this attribute will have no effect on the module initialization order.
    /// </summary>
    /// <seealso cref="RequiresAspNetCoreAttribute"/>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public sealed class UtilizesAspNetCoreAttribute : UtilizesAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UtilizesAspNetCoreAttribute"/> class. 
        /// </summary>
        public UtilizesAspNetCoreAttribute() : base(typeof(AspNetCoreModule)) { }
    }
}