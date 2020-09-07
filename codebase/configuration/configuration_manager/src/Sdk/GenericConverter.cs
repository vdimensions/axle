#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.ComponentModel;
using System.Globalization;
using Axle.Conversion.Parsing;

namespace Axle.Configuration.ConfigurationManager.Sdk
{
    /// <summary>
    /// A configuration converter class that can handle <see cref="Version" /> instances.
    /// </summary>
    public class GenericConverter<T, TParser> : ConfigurationConverter<T> where TParser: IParser<T>, new()
    {
        protected override T ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, string value)
        {
            var version = new TParser().Parse(value, culture);
            return version;
        }
        protected override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, T value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return value.ToString();
            }
            throw new InvalidOperationException(destinationType.FullName + " destination type is not supported.");
        }
    }
}
#endif