namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="int"/> and <see cref="float"/>.
    /// </summary>
    public sealed class Int32ToSingleConverter : AbstractTwoWayConverter<int, float>
    {
        /// <inheritdoc />
        protected override float DoConvert(int source) => source;

        /// <inheritdoc />
        protected override int DoConvertBack(float source) => (int) source;
    }
}