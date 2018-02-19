namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="long"/> and <see cref="double"/>.
    /// </summary>
    public sealed class Int64ToDoubleConverter : AbstractTwoWayConverter<long, double>
    {
        /// <inheritdoc />
        protected override double DoConvert(long source) => source;

        /// <inheritdoc />
        protected override long DoConvertBack(double source) => (long) source;
    }
}