namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="ulong"/> and <see cref="float"/>.
    /// </summary>
    public sealed class UInt64ToSingleConverter : AbstractTwoWayConverter<ulong, float>
    {
        /// <inheritdoc />
        protected override float DoConvert(ulong source) => source;

        /// <inheritdoc />
        protected override ulong DoConvertBack(float source) => (ulong) source;
    }
}