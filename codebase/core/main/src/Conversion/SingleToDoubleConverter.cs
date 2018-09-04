namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="float"/> and <see cref="double"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class SingleToDoubleConverter : AbstractTwoWayConverter<float, double>
    {
        /// <inheritdoc />
        protected override double DoConvert(float source) => source;

        /// <inheritdoc />
        protected override float DoConvertBack(double source) => (float) source;
    }
}