using System;
using System.Reflection;

#if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
using Axle.Environment;
#endif

namespace Axle.Reflection
{
    public sealed class AssemblyNameEqualityComparer : AdaptiveEqualityComparer<AssemblyName, string>
    {
        #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
        public AssemblyNameEqualityComparer() : base(x => x.Name, Platform.Environment.IsUnix() ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal) { }
        #else
        public AssemblyNameEqualityComparer() : base(x => x.Name, StringComparer.OrdinalIgnoreCase) { }
        #endif
    }
}