#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.ComponentModel;
using System.Globalization;
using Axle.Configuration.Legacy.Sdk;
using Axle.Conversion.Parsing;

namespace Axle.Configuration.Legacy
{
    /// <summary>
    /// A configuration converter class that can handle enum instances.
    /// </summary>
    /// <typeparam name="T">A valid enum type.</typeparam>
    public sealed class EnumNameConverter<T> : ConfigurationConverter<T> where T: struct, IComparable, IFormattable
    {
        private readonly EnumParser<T> enumParser = new EnumParser<T>();

        protected override T ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, string value)
        {
            return enumParser.Parse(value, culture);
        }
        protected override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, T value, Type destinationType)
        {
            return value.ToString("{0}", culture);
        }
    }
}
#endif