namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="uint"/> and <see cref="double"/>.
    /// </summary>
    public class UInt32ToDoubleConverter : AbstractTwoWayConverter<uint, double>
    {
        protected override double DoConvert(uint source) { return source; }

        protected override uint DoConvertBack(double source) { return (uint) source; }
    }
}