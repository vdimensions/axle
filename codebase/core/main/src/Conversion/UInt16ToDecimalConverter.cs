namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="ushort"/> and <see cref="decimal"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
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
