using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Axle.Conversion;
using Axle.Reflection.Extensions.Type;
using Axle.Verification;

namespace Axle.Resources
{
    /// <summary>
    /// A representation of textual resource.
    /// </summary>
    public sealed class TextResourceInfo : ResourceInfo
    {
        private readonly string _value;
        private readonly Encoding _encoding;

        /// <summary>
        /// Creates a new instance of the <seealso cref="TextResourceInfo"/> class.
        /// </summary>
        public TextResourceInfo(string name, CultureInfo culture, string value, Encoding encoding) : base(name, culture, "text/plain")
        {
            _value = value.VerifyArgument(nameof(value)).IsNotNull();
            _encoding = encoding ?? Encoding.UTF8;
        }
        /// <summary>
        /// Creates a new instance of the <seealso cref="TextResourceInfo"/> class.
        /// </summary>
        public TextResourceInfo(string name, CultureInfo culture, string value) : this(name, culture, value, null) { }

        /// <inheritdoc />
        public override Stream Open()
        {
            return new MemoryStream(new BytesToStringConverter(_encoding).ConvertBack(_value));
        }

        /// <inheritdoc />
        public override bool TryResolve(Type targetType, out object result)
        {
            if (targetType == typeof(string))
            {
                result = _value;
                return true;
            }
            if (targetType == typeof(StringBuilder))
            {
                result = new StringBuilder(_value);
                return true;
            }
            if (targetType == typeof(TextReader) || targetType == typeof(StringReader))
            {
                result = new StringReader(_value);
                return true;
            }
            if (targetType.ExtendsOrImplements<IEnumerable<char>>())
            {
                result = _value.ToCharArray();
                return true;
            }

            return base.TryResolve(targetType, out result);
        }

        /// <summary>
        /// Gets the text value that the current <see cref="TextResourceInfo"/> instance represents.
        /// </summary>
        public string Value => _value;
    }
}