﻿#if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
using System;
using System.Globalization;
using System.IO;

using Axle.Verification;


namespace Axle.Resources.Extraction.ResX
{
    internal sealed class ResXStreamResourceInfo : ResourceInfo
    {
        private readonly Uri _location;
        private readonly ResXResourceResolver _resolver;

        public ResXStreamResourceInfo(ResXResourceResolver resolver, Uri location, string name, CultureInfo culture) 
            : base(name, culture, "application/octet-stream")
        {
            _location = location.VerifyArgument(nameof(location)).IsNotNull();
            _resolver = resolver.VerifyArgument(nameof(resolver)).IsNotNull();
        }

        /// <inheritdoc />
        public override Stream Open()
        {
            return _resolver.Resolve(_location, Culture) as Stream;
        }
    }
}
#endif