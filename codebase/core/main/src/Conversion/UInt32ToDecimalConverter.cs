using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="uint"/> and <see cref="decimal"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class UInt32ToDecimalConverter : AbstractTwoWayConverter<uint, decimal>
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="UInt32ToDecimalConverter"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly UInt32ToDecimalConverter Instance = new UInt32ToDecimalConverter();
        
        /// <inheritdoc />
        protected override decimal DoConvert(uint source) => source;

        /// <inheritdoc />
        protected override uint DoConvertBack(decimal source) => (uint) source;
    }
}
