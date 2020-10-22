using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Web.AspNetCore.Session
{
    /// <summary>
    /// An attribute that, when applied on a module class, will establish the <see cref="AspNetCoreSessionModule"/> as a
    /// dependency on the target module.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresAspNetCoreSessionAttribute : RequiresAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiresAspNetCoreSessionAttribute"/> class. 
        /// </summary>
        public RequiresAspNetCoreSessionAttribute() : base(typeof(AspNetCoreSessionModule)) { }
    }
}