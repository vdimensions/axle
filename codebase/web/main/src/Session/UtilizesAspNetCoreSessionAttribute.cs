using System;
using System.Diagnostics.CodeAnalysis;

using Axle.Modularity;


namespace Axle.Web.AspNetCore.Session
{
    /// <summary>
    /// If the application uses AspNetCore session support, this attribute will cause that support for AspNetCore
    /// sessions will be made available before the target module initializes, similarly to the
    /// <see cref="RequiresAspNetCoreSessionAttribute"/> works.
    /// However, unlike the <see cref="RequiresAspNetCoreSessionAttribute"/>, this attribute will not trigger the
    /// initialization of AspNetCore sessions, making it an optional dependency. If AspNetCore sessions are not
    /// requested by another module or trough application config, this attribute will have no effect on the module
    /// initialization order.
    /// </summary>
    /// <seealso cref="RequiresAspNetCoreSessionAttribute"/>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class UtilizesAspNetCoreSessionAttribute : UtilizesAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UtilizesAspNetCoreSessionAttribute"/> class. 
        /// </summary>
        public UtilizesAspNetCoreSessionAttribute() : base(typeof(AspNetCoreSessionModule)) { }
    }
}