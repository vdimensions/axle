namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="ushort"/> and <see cref="decimal"/>.
    /// </summary>
    public sealed class UInt16ToDecimalConverter : AbstractTwoWayConverter<ushort, decimal>
    {
        /// <inheritdoc />
        protected override decimal DoConvert(ushort source) => source;

        /// <inheritdoc />
        protected override ushort DoConvertBack(decimal source) => (ushort) source;
    }
}