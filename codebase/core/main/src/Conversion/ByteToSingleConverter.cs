using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="byte"/> and <see cref="float"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class ByteToSingleConverter : AbstractTwoWayConverter<byte, float>
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="ByteToSingleConverter"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly ByteToSingleConverter Instance = new ByteToSingleConverter();
        
        /// <inheritdoc />
        protected override float DoConvert(byte source) => source;

        /// <inheritdoc />
        protected override byte DoConvertBack(float source) => (byte) source;
    }
}
