using System;


namespace Axle.Modularity
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class ModuleAttribute : Attribute { }
}