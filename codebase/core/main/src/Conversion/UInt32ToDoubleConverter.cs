namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="uint"/> and <see cref="double"/>.
    /// </summary>
    public sealed class UInt32ToDoubleConverter : AbstractTwoWayConverter<uint, double>
    {
        /// <inheritdoc />
        protected override double DoConvert(uint source) => source;

        /// <inheritdoc />
        protected override uint DoConvertBack(double source) => (uint) source;
    }
}