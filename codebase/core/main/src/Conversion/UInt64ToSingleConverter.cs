using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="ulong"/> and <see cref="float"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class UInt64ToSingleConverter : AbstractTwoWayConverter<ulong, float>
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="UInt64ToSingleConverter"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly UInt64ToSingleConverter Instance = new UInt64ToSingleConverter();
        
        /// <inheritdoc />
        protected override float DoConvert(ulong source) => source;

        /// <inheritdoc />
        protected override ulong DoConvertBack(float source) => (ulong) source;
    }
}
