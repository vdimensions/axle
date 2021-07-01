using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="uint"/> and <see cref="ulong"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class UInt16ToUInt64Converter : AbstractTwoWayConverter<ushort, ulong>
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="UInt16ToUInt64Converter"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly UInt16ToUInt64Converter Instance = new UInt16ToUInt64Converter();
        
        /// <inheritdoc />
        protected override ulong DoConvert(ushort source) => source;

        /// <inheritdoc />
        protected override ushort DoConvertBack(ulong source) => (ushort) source;
    }
}