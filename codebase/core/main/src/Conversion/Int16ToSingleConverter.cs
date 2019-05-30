namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="short"/> and <see cref="float"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class Int16ToSingleConverter : AbstractTwoWayConverter<short, float>
    {
        /// <inheritdoc />
        protected override float DoConvert(short source) => source;

        /// <inheritdoc />
        protected override short DoConvertBack(float source) => (short) source;
    }
}
