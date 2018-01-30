namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="int"/> and <see cref="double"/>.
    /// </summary>
    public class Int32ToDoubleConverter : AbstractTwoWayConverter<int, double>
    {
        protected override double DoConvert(int source) { return source; }

        protected override int DoConvertBack(double source) { return (int) source; }
    }
}