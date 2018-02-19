namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="short"/> and <see cref="decimal"/>.
    /// </summary>
    public sealed class Int16ToDecimalConverter : AbstractTwoWayConverter<short, decimal>
    {
        /// <inheritdoc />
        protected override decimal DoConvert(short source) => source;

        /// <inheritdoc />
        protected override short DoConvertBack(decimal source) => (short) source;
    }
}