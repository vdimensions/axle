#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.ComponentModel;
using System.Globalization;
using Axle.Configuration.ConfigurationManager.Sdk;
using Axle.Conversion.Parsing;

namespace Axle.Configuration.ConfigurationManager
{
    /// <summary>
    /// A <see cref="ConfigurationConverter{T}">configuration converter</see> implementation that can handle
    /// enum values.
    /// </summary>
    /// <typeparam name="T">
    /// A valid enum type.
    /// </typeparam>
    public sealed class EnumNameConverter<T> : ConfigurationConverter<T> where T: struct, IComparable, IFormattable
    {
        private readonly EnumParser<T> enumParser = new EnumParser<T>();

        /// <inheritdoc />
        protected override T ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, string value)
        {
            return enumParser.Parse(value, culture);
        }

        /// <inheritdoc />
        protected override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, T value, Type destinationType)
        {
            return value.ToString("{0}", culture);
        }
    }
}
#endif