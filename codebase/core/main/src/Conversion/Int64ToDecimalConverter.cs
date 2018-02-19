namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="long"/> and <see cref="decimal"/>.
    /// </summary>
    public sealed class Int64ToDecimalConverter : AbstractTwoWayConverter<long, decimal>
    {
        /// <inheritdoc />
        protected override decimal DoConvert(long source) => source;

        /// <inheritdoc />
        protected override long DoConvertBack(decimal source) => (long) source;
    }
}