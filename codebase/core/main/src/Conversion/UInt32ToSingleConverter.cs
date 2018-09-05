#if NETSTANDARD || NET35_OR_NEWER
namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="uint"/> and <see cref="float"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class UInt32ToSingleConverter : AbstractTwoWayConverter<uint, float>
    {
        /// <inheritdoc />
        protected override float DoConvert(uint source) => source;

        /// <inheritdoc />
        protected override uint DoConvertBack(float source) => (uint) source;
    }
}
#endif