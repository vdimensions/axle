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
        /// <inheritdoc />
        public sealed override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo culture, object value)
        {
            return ConvertFrom(ctx, culture, (string) value);
        }
        /// <summary>
        /// Converts the given <paramref name="value"/> to the type of this converter (<typeparamref name="T"/>), using
        /// the specified <paramref name="context" /> and <paramref name="culture"/> information.
        /// </summary>
        /// <param name="context">
        ///  An <see cref="ITypeDescriptorContext"/> that provides a format context.
        /// </param>
        /// <param name="culture">
        /// The <see cref="CultureInfo"/> to use as the current culture.
        /// </param>
        /// <param name="value">
        /// The <see cref="string"/> value to convert.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="T"/> that represents the converted value.
        /// </returns>
        protected abstract T ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, string value);

        /// <inheritdoc />
        public sealed override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return destinationType == value.GetType() ? value : ConvertTo(context, culture, (T) value, destinationType);
        }
        /// <summary>
        /// Converts the given <paramref name="value"/> object to the specified type, using the specified
        /// <paramref name="context"/> and <paramref name="culture"/> information.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture">
        /// A <see cref="CultureInfo"/>. If null is passed, the <see cref="CultureInfo.CurrentCulture">current culture</see> is assumed.
        /// </param>
        /// <param name="value">
        /// The <typeparamref name="T"/> instance to convert.
        /// </param>
        /// <param name="destinationType">
        /// The <see cref="Type"/> to convert the <paramref name="value"/> parameter to.
        /// </param>
        /// <returns>
        /// An <see cref="object"/> that represents the converted value.
        /// </returns>
        protected abstract object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, T value, Type destinationType);
    }
}
#endif