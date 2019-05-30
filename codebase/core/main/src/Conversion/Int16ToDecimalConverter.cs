namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="short"/> and <see cref="decimal"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class Int16ToDecimalConverter : AbstractTwoWayConverter<short, decimal>
    {
        /// <inheritdoc />
        protected override decimal DoConvert(short source) => source;

        /// <inheritdoc />
        protected override short DoConvertBack(decimal source) => (short) source;
    }
}
