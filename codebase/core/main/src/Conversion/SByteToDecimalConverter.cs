#if NETSTANDARD || NET20_OR_NEWER
namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="sbyte"/> and <see cref="decimal"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class SByteToDecimalConverter : AbstractTwoWayConverter<sbyte, decimal>
    {
        /// <inheritdoc />
        protected override decimal DoConvert(sbyte source) => source;

        /// <inheritdoc />
        protected override sbyte DoConvertBack(decimal source) => (sbyte) source;
    }
}
#endif