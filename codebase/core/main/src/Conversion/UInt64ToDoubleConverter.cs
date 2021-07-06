using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="ulong"/> and <see cref="double"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class UInt64ToDoubleConverter : AbstractTwoWayConverter<ulong, double>
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="UInt64ToDoubleConverter"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly UInt64ToDoubleConverter Instance = new UInt64ToDoubleConverter();
        
        /// <inheritdoc />
        protected override double DoConvert(ulong source) => source;

        /// <inheritdoc />
        protected override ulong DoConvertBack(double source) => (ulong) source;
    }
}
