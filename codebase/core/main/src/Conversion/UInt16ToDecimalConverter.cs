#if NETSTANDARD || NET20_OR_NEWER
namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="ushort"/> and <see cref="decimal"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class UInt16ToDecimalConverter : AbstractTwoWayConverter<ushort, decimal>
    {
        /// <inheritdoc />
        protected override decimal DoConvert(ushort source) => source;

        /// <inheritdoc />
        protected override ushort DoConvertBack(decimal source) => (ushort) source;
    }
}
#endif