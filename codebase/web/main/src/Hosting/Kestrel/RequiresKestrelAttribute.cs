using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Web.AspNetCore.Hosting.Kestrel
{
    /// <summary>
    /// An attribute that, when applied on a module class, will establish the <see cref="KestrelServerModule"/> as a
    /// dependency on the target module.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresKestrelAttribute : RequiresAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiresKestrelAttribute"/> class.
        /// </summary>
        public RequiresKestrelAttribute() : base(typeof(KestrelServerModule)) { }
    }
}