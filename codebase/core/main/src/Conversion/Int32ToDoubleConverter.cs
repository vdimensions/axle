namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="int"/> and <see cref="double"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class Int32ToDoubleConverter : AbstractTwoWayConverter<int, double>
    {
        /// <inheritdoc />
        protected override double DoConvert(int source) => source;

        /// <inheritdoc />
        protected override int DoConvertBack(double source) => (int) source;
    }
}
