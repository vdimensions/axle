#if NETSTANDARD || NET20_OR_NEWER
namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="short"/> and <see cref="double"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class Int16ToDoubleConverter : AbstractTwoWayConverter<short, double>
    {
        /// <inheritdoc />
        protected override double DoConvert(short source) => source;

        /// <inheritdoc />
        protected override short DoConvertBack(double source) => (short) source;
    }
}
#endif