using System;
using System.Diagnostics.CodeAnalysis;


namespace Axle.Modularity
{
    [Obsolete("Use `ReportsToAttribute` instead")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = true)]
    public class UtilizedByAttribute : ReportsToAttribute
    {
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public UtilizedByAttribute(string module) : base(module) { }
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public UtilizedByAttribute(Type moduleType) : base(moduleType) { }
    }
}