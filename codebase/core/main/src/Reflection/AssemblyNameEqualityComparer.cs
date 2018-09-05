#if NETSTANDARD || NET35_OR_NEWER
using System;
using System.Reflection;

#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using Axle.Environment;
#endif

namespace Axle.Reflection
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class AssemblyNameEqualityComparer : AdaptiveEqualityComparer<AssemblyName, string>
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        public AssemblyNameEqualityComparer() : base(x => x.Name, Platform.Environment.IsUnix() ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal) { }
        #else
        public AssemblyNameEqualityComparer() : base(x => x.Name, StringComparer.OrdinalIgnoreCase) { }
        #endif
    }
}
#endif