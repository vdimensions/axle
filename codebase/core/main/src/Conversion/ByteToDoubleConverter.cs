namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="byte"/> and <see cref="double"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [System.Serializable]
    #endif
    public sealed class ByteToDoubleConverter : AbstractTwoWayConverter<byte, double>
    {
        /// <inheritdoc />
        protected override double DoConvert(byte source) => source;

        /// <inheritdoc />
        protected override byte DoConvertBack(double source) => (byte) source;
    }
}
