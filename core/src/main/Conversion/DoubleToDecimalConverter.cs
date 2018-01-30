namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="double"/> and <see cref="decimal"/>.
    /// </summary>
    public class DoubleToDecimalConverter : AbstractTwoWayConverter<double, decimal>
    {
        protected override decimal DoConvert(double source) { return new decimal(source); }

        protected override double DoConvertBack(decimal source) { return (double) source; }
    }
}