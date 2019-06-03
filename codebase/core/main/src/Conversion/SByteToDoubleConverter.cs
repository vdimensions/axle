namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="sbyte"/> and <see cref="double"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class SByteToDoubleConverter : AbstractTwoWayConverter<sbyte, double>
    {
        /// <inheritdoc />
        protected override double DoConvert(sbyte source) => source;

        /// <inheritdoc />
        protected override sbyte DoConvertBack(double source) => (sbyte) source;
    }
}
