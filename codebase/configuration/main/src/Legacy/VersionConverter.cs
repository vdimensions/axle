#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Configuration.Legacy.Sdk;
using Axle.Conversion.Parsing;

namespace Axle.Configuration.Legacy
{
    /// <summary>
    /// A configuration converter class that can handle <see cref="Version" /> instances.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class VersionConverter : GenericConverter<Version, VersionParser> { }
}
#endif