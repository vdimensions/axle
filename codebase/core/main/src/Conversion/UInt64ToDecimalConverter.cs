using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="ulong"/> and <see cref="decimal"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class UInt64ToDecimalConverter : AbstractTwoWayConverter<ulong, decimal>
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="UInt64ToDecimalConverter"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly UInt64ToDecimalConverter Instance = new UInt64ToDecimalConverter();
        
        /// <inheritdoc />
        protected override decimal DoConvert(ulong source) => source;

        /// <inheritdoc />
        protected override ulong DoConvertBack(decimal source) => (ulong) source;
    }
}
