namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="ulong"/> and <see cref="double"/>.
    /// </summary>
    public class UInt64ToDoubleConverter : AbstractTwoWayConverter<ulong, double>
    {
        protected override double DoConvert(ulong source) { return source; }

        protected override ulong DoConvertBack(double source) { return (ulong) source; }
    }
}