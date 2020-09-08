#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Axle.Configuration.ConfigurationManager.Sdk;
using Axle.Conversion.Parsing;

namespace Axle.Configuration.ConfigurationManager
{
    /// <summary>
    /// A configuration converter class that can handle <see cref="Assembly" /> values.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class AssemblyNameConverter : GenericConverter<Assembly, AssemblyParser> { }
}
#endif