#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Configuration.ConfigurationManager.Sdk;
using Axle.Conversion.Parsing;

namespace Axle.Configuration.ConfigurationManager
{
    /// <summary>
    /// A configuration converter class that can handle <see cref="Version" /> values.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class VersionConverter : GenericConverter<Version, VersionParser> { }
}
#endif