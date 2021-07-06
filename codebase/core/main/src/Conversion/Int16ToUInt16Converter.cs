namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="short"/> and <see cref="ushort"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class Int16ToUInt16Converter : AbstractTwoWayConverter<short, ushort>
    {
        /// <inheritdoc />
        protected override ushort DoConvert(short source) => (ushort) source;

        /// <inheritdoc />
        protected override short DoConvertBack(ushort source) => (short) source;
    }
}