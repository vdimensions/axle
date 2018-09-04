namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="long"/> and <see cref="decimal"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class Int64ToDecimalConverter : AbstractTwoWayConverter<long, decimal>
    {
        /// <inheritdoc />
        protected override decimal DoConvert(long source) => source;

        /// <inheritdoc />
        protected override long DoConvertBack(decimal source) => (long) source;
    }
}