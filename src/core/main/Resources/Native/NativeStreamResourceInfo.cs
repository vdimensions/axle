using System;
using System.Globalization;
using System.IO;

using Axle.Verification;


namespace Axle.Resources.Native
{
    public sealed class NativeStreamResourceInfo : ResourceInfo
    {
        private readonly NativeResourceResolver _resolver;

        internal NativeStreamResourceInfo(NativeResourceResolver resolver, Uri resourceKey, CultureInfo culture) 
            : base(resourceKey, culture, "application/octet-stream")
        {
            _resolver = resolver.VerifyArgument(nameof(resolver)).IsNotNull();
        }

        public override Stream Open()
        {
            return _resolver.Resolve(Key.ToString(), Culture) as Stream;
        }
    }
}