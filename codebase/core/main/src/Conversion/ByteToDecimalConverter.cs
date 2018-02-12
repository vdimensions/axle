namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="byte"/> and <see cref="decimal"/>.
    /// </summary>
    public class ByteToDecimalConverter : AbstractTwoWayConverter<byte, decimal>
    {
        protected override decimal DoConvert(byte source) { return source; }

        protected override byte DoConvertBack(decimal source) { return (byte) source; }
    }
}