#if NETSTANDARD || NET20_OR_NEWER
namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="ulong"/> and <see cref="double"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class UInt64ToDoubleConverter : AbstractTwoWayConverter<ulong, double>
    {
        /// <inheritdoc />
        protected override double DoConvert(ulong source) => source;

        /// <inheritdoc />
        protected override ulong DoConvertBack(double source) => (ulong) source;
    }
}
#endif