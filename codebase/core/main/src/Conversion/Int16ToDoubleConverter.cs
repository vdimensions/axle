namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="short"/> and <see cref="double"/>.
    /// </summary>
    public sealed class Int16ToDoubleConverter : AbstractTwoWayConverter<short, double>
    {
        /// <inheritdoc />
        protected override double DoConvert(short source) => source;

        /// <inheritdoc />
        protected override short DoConvertBack(double source) => (short) source;
    }
}