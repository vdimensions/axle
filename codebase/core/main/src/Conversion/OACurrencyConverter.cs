using System.Diagnostics.CodeAnalysis;

#if NETSTANDARD2_0_OR_NEWER || NET40_OR_NEWER
namespace Axle.Conversion
{
    /// <summary>
    /// A converter to and from <see cref="decimal"/> to an OLE Automation currency format.
    /// </summary>
    /// <seealso cref="decimal.FromOACurrency"/>
    /// <seealso cref="decimal.ToOACurrency"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class OACurrencyConverter : AbstractTwoWayConverter<decimal, long>
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="OACurrencyConverter"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly OACurrencyConverter Instance = new OACurrencyConverter();

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
        protected override long DoConvert(decimal value) => decimal.ToOACurrency(value);

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
        protected override decimal DoConvertBack(long value) => decimal.FromOACurrency(value);
    }
}
#endif