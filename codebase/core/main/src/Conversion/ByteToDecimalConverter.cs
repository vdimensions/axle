using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="byte"/> and <see cref="decimal"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class ByteToDecimalConverter : AbstractTwoWayConverter<byte, decimal>
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="ByteToDecimalConverter"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly ByteToDecimalConverter Instance = new ByteToDecimalConverter();
        
        /// <inheritdoc />
        protected override decimal DoConvert(byte source) => source;

        /// <inheritdoc />
        protected override byte DoConvertBack(decimal source) => (byte) source;
    }
}
