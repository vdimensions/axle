namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="sbyte"/> and <see cref="decimal"/>.
    /// </summary>
    public sealed class SByteToDecimalConverter : AbstractTwoWayConverter<sbyte, decimal>
    {
        /// <inheritdoc />
        protected override decimal DoConvert(sbyte source) => source;

        /// <inheritdoc />
        protected override sbyte DoConvertBack(decimal source) => (sbyte) source;
    }
}