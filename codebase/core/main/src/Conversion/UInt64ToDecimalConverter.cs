namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="ulong"/> and <see cref="decimal"/>.
    /// </summary>
    public sealed class UInt64ToDecimalConverter : AbstractTwoWayConverter<ulong, decimal>
    {
        /// <inheritdoc />
        protected override decimal DoConvert(ulong source) => source;

        /// <inheritdoc />
        protected override ulong DoConvertBack(decimal source) => (ulong) source;
    }
}