using System;
using System.Globalization;
using System.IO;

using Axle.Conversion;
using Axle.Extensions.Globalization.TextInfo;
using Axle.Verification;


namespace Axle.Resources
{
    /// <summary>
    /// A representation of textual resource.
    /// </summary>
    public sealed class TextResourceInfo : ResourceInfo
    {
        private readonly string _value;

        /// <summary>
        /// Creates a new instance of the <seealso cref="TextResourceInfo"/> class.
        /// </summary>
        public TextResourceInfo(Uri location, string name, CultureInfo culture, string value) : base(location, name, culture, "text/plain")
        {
            _value = value.VerifyArgument(nameof(value)).IsNotNull();
        }

        /// <inheritdoc />
        public override Stream Open()
        {
            return new MemoryStream(new BytesToStringConverter(Culture.TextInfo.GetEncoding()).ConvertBack(_value));
        }

        public string Value => _value;
    }
}