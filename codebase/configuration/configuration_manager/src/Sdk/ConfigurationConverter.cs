#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace Axle.Configuration.ConfigurationManager.Sdk
{
    /// <summary>
    /// A generic base class for the configuration converter types.
    /// </summary>
    /// <typeparam name="T">The type of the configuration property this converter can handle.</typeparam>
    /// <seealso cref="ConfigurationConverterBase"/>
    public abstract class ConfigurationConverter<T> : ConfigurationConverterBase
    {
        public sealed override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo culture, object value)
        {
            return ConvertFrom(ctx, culture, (string) value);
        }
        protected abstract T ConvertFrom(ITypeDescriptorContext ctx, CultureInfo culture, string value);

        public sealed override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return destinationType.Equals(value.GetType()) ? value : ConvertTo(context, culture, (T) value, destinationType);
        }
        protected abstract object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, T value, Type destinationType);
    }
}
#endif