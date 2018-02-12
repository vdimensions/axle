namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="byte"/> and <see cref="float"/>.
    /// </summary>
    public class ByteToSingleConverter : AbstractTwoWayConverter<byte, float>
    {
        protected override float DoConvert(byte source) { return source; }

        protected override byte DoConvertBack(float source) { return (byte) source; }
    }
}