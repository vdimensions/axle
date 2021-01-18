namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="long"/> and <see cref="ulong"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [System.Serializable]
    #endif
    public sealed class Int64ToUInt64Converter : AbstractTwoWayConverter<long, ulong>
    {
        /// <inheritdoc />
        protected override ulong DoConvert(long source) => (ulong) source;

        /// <inheritdoc />
        protected override long DoConvertBack(ulong source) => (long) source;
    }
}