﻿using System;
using System.Globalization;
using System.IO;

using Axle.Verification;


namespace Axle.Resources.Native
{
    internal sealed class NativeStreamResourceInfo : ResourceInfo
    {
        private readonly NativeResourceResolver _resolver;

        public NativeStreamResourceInfo(NativeResourceResolver resolver, Uri resourceKey, CultureInfo culture) 
            : base(resourceKey, culture, "application/octet-stream")
        {
            _resolver = resolver.VerifyArgument(nameof(resolver)).IsNotNull();
        }

        /// <inheritdoc />
        public override Stream Open()
        {
            return _resolver.Resolve(Key.ToString(), Culture) as Stream;
        }
    }
}