#if NET40_OR_NEWER || NETSTANDARD2_0_OR_NEWER
namespace Axle.Conversion
{
    /// <summary>
    /// A converter to and from <see cref="decimal"/> to an OLE Automation currency format.
    /// </summary>
    /// <seealso cref="decimal.FromOACurrency"/>
    /// <seealso cref="decimal.ToOACurrency"/>
    public class OACurrencyConverter : AbstractTwoWayConverter<decimal, long>
    {
        /// <summary>
        /// Converts the specified <see cref="decimal"/> value to the equivalent OLE Automation Currency value, 
        /// which is contained in a <see cref="long">64-bit signed integer</see>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="decimal"/> number to convert
        /// </param>
        /// <returns>
        /// A <see cref="long">64-bit signed integer</see> that contains the OLE Automation equivalent of <paramref name="value"/>.
        /// </returns>
        protected override long DoConvert(decimal value)
        {
            return decimal.ToOACurrency(value);
        }

        /// <summary>
        /// Converts the specified <see cref="long">64-bit signed integer</see>, which contains an OLE Automation Currency value, 
        /// to the equivalent <see cref="decimal"/> value.
        /// </summary>
        /// <param name="value">
        /// An OLE Automation Currency value. 
        /// </param>
        /// <returns>
        /// A <see cref="decimal"/> that contains the equivalent of <paramref name="value"/>.
        /// </returns>
        protected override decimal DoConvertBack(long value)
        {
            return decimal.FromOACurrency(value);
        }
    }
}
#endif