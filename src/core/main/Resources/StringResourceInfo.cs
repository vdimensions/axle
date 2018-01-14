using System;
using System.Globalization;
using System.IO;

using Axle.Conversion;
using Axle.Extensions.Globalization.TextInfo;
using Axle.Verification;


namespace Axle.Resources
{
    public sealed class StringResourceInfo : ResourceInfo
    {
        private readonly string _value;

        public StringResourceInfo(Uri key, CultureInfo culture, string value) : base(key, culture, "text/plain")
        {
            _value = value.VerifyArgument(nameof(value)).IsNotNull();
        }

        public override Stream Open()
        {
            return new MemoryStream(new BytesToStringConverter(Culture.TextInfo.GetEncoding()).ConvertBack(_value));
        }

        public object Value => _value;
    }
}