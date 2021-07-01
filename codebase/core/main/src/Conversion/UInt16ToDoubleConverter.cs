using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="ushort"/> and <see cref="double"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class UInt16ToDoubleConverter : AbstractTwoWayConverter<ushort, double>
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="UInt16ToDoubleConverter"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly UInt16ToDoubleConverter Instance = new UInt16ToDoubleConverter();
        
        /// <inheritdoc />
        protected override double DoConvert(ushort source) => source;

        /// <inheritdoc />
        protected override ushort DoConvertBack(double source) => (ushort) source;
    }
}
