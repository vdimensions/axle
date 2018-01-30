namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="long"/> and <see cref="double"/>.
    /// </summary>
    public class Int64ToDoubleConverter : AbstractTwoWayConverter<long, double>
    {
        protected override double DoConvert(long source) { return source; }

        protected override long DoConvertBack(double source) { return (long) source; }
    }
}