#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Configuration.ConfigurationManager.Sdk;

namespace Axle.Configuration.ConfigurationManager
{
    using UriParser = Conversion.Parsing.UriParser;

    /// <summary>
    /// A configuration converter class that can handle <see cref="Uri" /> values.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class UriConverter : GenericConverter<Uri, UriParser> { }
}
#endif