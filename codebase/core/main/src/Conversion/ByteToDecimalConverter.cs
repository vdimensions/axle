namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="byte"/> and <see cref="decimal"/>.
    /// </summary>
    public sealed class ByteToDecimalConverter : AbstractTwoWayConverter<byte, decimal>
    {
        /// <inheritdoc />
        protected override decimal DoConvert(byte source) => source;

        /// <inheritdoc />
        protected override byte DoConvertBack(decimal source) => (byte) source;
    }
}