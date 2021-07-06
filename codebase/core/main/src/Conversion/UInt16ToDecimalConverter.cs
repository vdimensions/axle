using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="ushort"/> and <see cref="decimal"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class UInt16ToDecimalConverter : AbstractTwoWayConverter<ushort, decimal>
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="UInt16ToDecimalConverter"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly UInt16ToDecimalConverter Instance = new UInt16ToDecimalConverter();
        
        /// <inheritdoc />
        protected override decimal DoConvert(ushort source) => source;

        /// <inheritdoc />
        protected override ushort DoConvertBack(decimal source) => (ushort) source;
    }
}
