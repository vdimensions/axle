using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="ushort"/> and <see cref="float"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class UInt16ToSingleConverter : AbstractTwoWayConverter<ushort, float>
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="UInt16ToSingleConverter"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly UInt16ToSingleConverter Instance = new UInt16ToSingleConverter();
        
        /// <inheritdoc />
        protected override float DoConvert(ushort source) => source;

        /// <inheritdoc />
        protected override ushort DoConvertBack(float source) => (ushort) source;
    }
}
