namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="double"/> and <see cref="decimal"/>.
    /// </summary>
    public sealed class DoubleToDecimalConverter : AbstractTwoWayConverter<double, decimal>
    {
        /// <inheritdoc />
        protected override decimal DoConvert(double source) => new decimal(source);

        /// <inheritdoc />
        protected override double DoConvertBack(decimal source) => (double) source;
    }
}