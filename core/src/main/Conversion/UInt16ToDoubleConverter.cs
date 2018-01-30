namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="ushort"/> and <see cref="double"/>.
    /// </summary>
    public class UInt16ToDoubleConverter : AbstractTwoWayConverter<ushort, double>
    {
        protected override double DoConvert(ushort source) { return source; }

        protected override ushort DoConvertBack(double source) { return (ushort) source; }
    }
}