using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="uint"/> and <see cref="ulong"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class UInt32ToUInt64Converter : AbstractTwoWayConverter<uint, ulong>
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="UInt32ToUInt64Converter"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly UInt32ToUInt64Converter Instance = new UInt32ToUInt64Converter();
        
        /// <inheritdoc />
        protected override ulong DoConvert(uint source) => source;

        /// <inheritdoc />
        protected override uint DoConvertBack(ulong source) => (uint) source;
    }
}