namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="ushort"/> and <see cref="double"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class UInt16ToDoubleConverter : AbstractTwoWayConverter<ushort, double>
    {
        /// <inheritdoc />
        protected override double DoConvert(ushort source) => source;

        /// <inheritdoc />
        protected override ushort DoConvertBack(double source) => (ushort) source;
    }
}