namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="int"/> and <see cref="uint"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [System.Serializable]
    #endif
    public sealed class Int32ToUInt32Converter : AbstractTwoWayConverter<int, uint>
    {
        /// <inheritdoc />
        protected override uint DoConvert(int source) => (uint) source;

        /// <inheritdoc />
        protected override int DoConvertBack(uint source) => (int) source;
    }
}