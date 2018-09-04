namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="ulong"/> and <see cref="decimal"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class UInt64ToDecimalConverter : AbstractTwoWayConverter<ulong, decimal>
    {
        /// <inheritdoc />
        protected override decimal DoConvert(ulong source) => source;

        /// <inheritdoc />
        protected override ulong DoConvertBack(decimal source) => (ulong) source;
    }
}