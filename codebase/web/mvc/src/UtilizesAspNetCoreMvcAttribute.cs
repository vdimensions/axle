using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Web.AspNetCore.Mvc
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class UtilizesAspNetCoreMvcAttribute : UtilizesAttribute
    {
        public UtilizesAspNetCoreMvcAttribute() : base(typeof(AspNetCoreMvcModule)) { }
    }
}