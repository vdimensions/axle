namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="short"/> and <see cref="double"/>.
    /// </summary>
    public class Int16ToDoubleConverter : AbstractTwoWayConverter<short, double>
    {
        protected override double DoConvert(short source) { return source; }

        protected override short DoConvertBack(double source) { return (short) source; }
    }
}