using System;
using System.Diagnostics.CodeAnalysis;

using Axle.Modularity;


namespace Axle.Web.WebSharper.Sitelets
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresWebSharperSiteletsAttribute : RequiresAttribute
    {
        public RequiresWebSharperSiteletsAttribute() : base(typeof(WebSharperSiteletsModule)) { }
    }
}