#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Globalization;
using System.IO;
using Axle.Resources.ResX.Extraction;
using Axle.Verification;

namespace Axle.Resources.ResX
{
    internal sealed class ResXStreamResourceInfo : ResourceInfo
    {
        private readonly Uri _location;
        private readonly ResXResourceResolver _resolver;

        internal ResXStreamResourceInfo(ResXResourceResolver resolver, Uri location, string name, CultureInfo culture) 
            : base(name, culture, "application/octet-stream")
        {
            _location = location.VerifyArgument(nameof(location)).IsNotNull();
            _resolver = resolver.VerifyArgument(nameof(resolver)).IsNotNull();
        }

        public override Stream Open() => _resolver.Resolve(_location, Culture) as Stream;
    }
}
#endif