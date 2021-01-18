namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="ushort"/> and <see cref="float"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [System.Serializable]
    #endif
    public sealed class UInt16ToSingleConverter : AbstractTwoWayConverter<ushort, float>
    {
        /// <inheritdoc />
        protected override float DoConvert(ushort source) => source;

        /// <inheritdoc />
        protected override ushort DoConvertBack(float source) => (ushort) source;
    }
}
