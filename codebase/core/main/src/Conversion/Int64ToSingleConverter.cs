namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="long"/> and <see cref="float"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class Int64ToSingleConverter : AbstractTwoWayConverter<long, float>
    {
        /// <inheritdoc />
        protected override float DoConvert(long source) => source;

        /// <inheritdoc />
        protected override long DoConvertBack(float source) => (long) source;
    }
}