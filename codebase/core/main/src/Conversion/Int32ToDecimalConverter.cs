#if NETSTANDARD || NET35_OR_NEWER
namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="int"/> and <see cref="decimal"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class Int32ToDecimalConverter : AbstractTwoWayConverter<int, decimal>
    {
        /// <inheritdoc />
        protected override decimal DoConvert(int source) => source;

        /// <inheritdoc />
        protected override int DoConvertBack(decimal source) => (int) source;
    }
}
#endif