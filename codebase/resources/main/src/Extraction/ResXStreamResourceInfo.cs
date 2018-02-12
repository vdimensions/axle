#if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
using System;
using System.Globalization;
using System.IO;

using Axle.Verification;


namespace Axle.Resources.Extraction
{
    internal sealed class ResXStreamResourceInfo : ResourceInfo
    {
        private readonly ResXResourceResolver _resolver;

        public ResXStreamResourceInfo(ResXResourceResolver resolver, Uri location, string name, CultureInfo culture) 
            : base(location, name, culture, "application/octet-stream")
        {
            _resolver = resolver.VerifyArgument(nameof(resolver)).IsNotNull();
        }

        /// <inheritdoc />
        public override Stream Open()
        {
            return _resolver.Resolve(Location, Culture) as Stream;
        }
    }
}
#endif