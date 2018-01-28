using System;
using System.Reflection;


namespace Axle.Reflection
{
    public sealed class AssemblyNameEqualityComparer : AdaptiveEqualityComparer<AssemblyName, string>
    {
        public AssemblyNameEqualityComparer() : base(x => x.Name, StringComparer.OrdinalIgnoreCase) { }
    }
}