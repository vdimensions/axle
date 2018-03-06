using System;

using Axle.Configuration.Sdk;
using Axle.Conversion.Parsing;


namespace Axle.Configuration
{
    /// <summary>
    /// A configuration converter class that can handle <see cref="Version" /> instances.
    /// </summary>
    public sealed class VersionConverter : GenericConverter<Version, VersionParser> { }
}