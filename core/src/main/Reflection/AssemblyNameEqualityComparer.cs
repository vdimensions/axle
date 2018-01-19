using System;
using System.Reflection;

using Axle.Environment;


namespace Axle.Reflection
{
    public sealed class AssemblyNameEqualityComparer : AdaptiveEqualityComparer<AssemblyName, string>
    {
        public AssemblyNameEqualityComparer() : base(x => x.Name, Platform.Environment.IsUnix() ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal) { }
    }
}