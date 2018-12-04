using System;

using Axle.Modularity;


namespace Axle.Web.WebSharper.Forest
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresWebSharperForestAttribute : RequiresAttribute
    {
        public RequiresWebSharperForestAttribute() : base(typeof(WebSharperForestModule)) { }
    }
}