using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="uint"/> and <see cref="float"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class UInt32ToSingleConverter : AbstractTwoWayConverter<uint, float>
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="UInt32ToSingleConverter"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly UInt32ToSingleConverter Instance = new UInt32ToSingleConverter();
        
        /// <inheritdoc />
        protected override float DoConvert(uint source) => source;

        /// <inheritdoc />
        protected override uint DoConvertBack(float source) => (uint) source;
    }
}
