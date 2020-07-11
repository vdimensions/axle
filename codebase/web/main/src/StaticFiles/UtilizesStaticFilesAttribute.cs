using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Web.AspNetCore.StaticFiles
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class UtilizesStaticFilesAttribute : UtilizesAttribute
    {
        public UtilizesStaticFilesAttribute() : base(typeof(StaticFilesModule)) { }
    }
}