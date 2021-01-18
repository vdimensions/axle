namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="sbyte"/> and <see cref="float"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [System.Serializable]
    #endif
    public sealed class SByteToSingleConverter : AbstractTwoWayConverter<sbyte, float>
    {
        /// <inheritdoc />
        protected override float DoConvert(sbyte source) => source;

        /// <inheritdoc />
        protected override sbyte DoConvertBack(float source) => (sbyte) source;
    }
}
