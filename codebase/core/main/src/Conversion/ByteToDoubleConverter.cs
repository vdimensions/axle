namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="byte"/> and <see cref="double"/>.
    /// </summary>
    public sealed class ByteToDoubleConverter : AbstractTwoWayConverter<byte, double>
    {
        /// <inheritdoc />
        protected override double DoConvert(byte source) => source;

        /// <inheritdoc />
        protected override byte DoConvertBack(double source) => (byte) source;
    }
}