namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="byte"/> and <see cref="float"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class ByteToSingleConverter : AbstractTwoWayConverter<byte, float>
    {
        /// <inheritdoc />
        protected override float DoConvert(byte source) => source;

        /// <inheritdoc />
        protected override byte DoConvertBack(float source) => (byte) source;
    }
}