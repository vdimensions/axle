using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Web.AspNetCore.FileServer
{
    /// <summary>
    /// An attribute that, when applied on a module class, will establish the <see cref="StaticFilesModule"/> as an
    /// optional dependency on the target module. The target module will remain unaffected in case the
    /// <see cref="StaticFilesModule"/> was not explicitly configured for loading by the application.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class UtilizesStaticFilesAttribute : UtilizesAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UtilizesStaticFilesAttribute"/> class.
        /// </summary>
        public UtilizesStaticFilesAttribute() : base(typeof(StaticFilesModule)) { }
    }
}