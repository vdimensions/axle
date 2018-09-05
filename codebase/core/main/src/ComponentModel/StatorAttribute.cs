#if NETSTANDARD || NET35_OR_NEWER
using System;
using System.Diagnostics;


namespace Axle.ComponentModel
{
    [Conditional("STATOR")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    internal sealed class ComponentStatorAttribute : Attribute
    {
        public ComponentStatorAttribute()
        {
        }
    }
}
#endif
