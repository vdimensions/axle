using System;
using System.Reflection;

#if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
using Axle.Environment;
#endif

namespace Axle.Reflection
{
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    [Serializable]
    #endif
    public sealed class AssemblyNameEqualityComparer : AdaptiveEqualityComparer<AssemblyName, string>
    {
        #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
        public AssemblyNameEqualityComparer() : base(x => x.Name, Platform.Environment.IsUnix() ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal) { }
        #else
        public AssemblyNameEqualityComparer() : base(x => x.Name, StringComparer.OrdinalIgnoreCase) { }
        #endif
    }
}