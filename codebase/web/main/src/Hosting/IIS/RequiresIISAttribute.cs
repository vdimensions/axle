using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Web.AspNetCore.Hosting.IIS
{
    /// <summary>
    /// An attribute that, when applied on a module class, will establish the <see cref="IISIntegrationModule"/> as a
    /// dependency on the target module. This essentially enables the target web application to be hosted inside
    /// the IIS web server.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresIISAttribute : RequiresAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiresIISAttribute"/> class.
        /// </summary>
        public RequiresIISAttribute() : base(typeof(IISIntegrationModule)) { }
    }
}