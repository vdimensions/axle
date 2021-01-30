using System;
using System.Reflection;

#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using Axle.Environment;
#endif

namespace Axle.Reflection
{
    /// <summary>
    /// An equality comparer for assembly name objects, that uses case-insensitive name comparison depending
    /// on the target OS. 
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class AssemblyNameEqualityComparer : AdaptiveEqualityComparer<AssemblyName, string>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="AssemblyNameEqualityComparer"/> class. 
        /// </summary>
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        public AssemblyNameEqualityComparer() : base(x => x.Name, EnvironmentExtensions.IsUnix(Platform.Environment) ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal) { }
        #else
        public AssemblyNameEqualityComparer() : base(x => x.Name, StringComparer.OrdinalIgnoreCase) { }
        #endif
    }
}