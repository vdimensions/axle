#if NETSTANDARD || NET35_OR_NEWER
namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="int"/> and <see cref="float"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class Int32ToSingleConverter : AbstractTwoWayConverter<int, float>
    {
        /// <inheritdoc />
        protected override float DoConvert(int source) => source;

        /// <inheritdoc />
        protected override int DoConvertBack(float source) => (int) source;
    }
}
#endif