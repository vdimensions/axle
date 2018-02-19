namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="uint"/> and <see cref="decimal"/>.
    /// </summary>
    public sealed class UInt32ToDecimalConverter : AbstractTwoWayConverter<uint, decimal>
    {
        /// <inheritdoc />
        protected override decimal DoConvert(uint source) => source;

        /// <inheritdoc />
        protected override uint DoConvertBack(decimal source) => (uint) source;
    }
}