using System.Reflection;

using Axle.Configuration.Sdk;
using Axle.Conversion.Parsing;


namespace Axle.Configuration
{
    /// <summary>
    /// A configuration converter class that can handle <see cref="Assembly" /> instances.
    /// </summary>
    public sealed class AssemblyNameConverter : GenericConverter<Assembly, AssemblyParser> { }
}
