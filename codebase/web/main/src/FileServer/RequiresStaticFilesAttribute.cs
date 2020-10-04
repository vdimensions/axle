using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Web.AspNetCore.FileServer
{
    /// <summary>
    /// An attribute that, when applied on a module class, will establish the <see cref="StaticFilesModule"/> as a
    /// dependency on the target module.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresStaticFilesAttribute : RequiresAttribute
    {
        /// <summary>
        /// Initialized a new instance of the <see cref="RequiresStaticFilesAttribute"/> class.
        /// </summary>
        public RequiresStaticFilesAttribute() : base(typeof(StaticFilesModule)) { }
    }
}