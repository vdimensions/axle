namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="byte"/> and <see cref="double"/>.
    /// </summary>
    public class ByteToDoubleConverter : AbstractTwoWayConverter<byte, double>
    {
        protected override double DoConvert(byte source) { return source; }

        protected override byte DoConvertBack(double source) { return (byte) source; }
    }
}