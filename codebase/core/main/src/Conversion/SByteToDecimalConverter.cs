using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="sbyte"/> and <see cref="decimal"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class SByteToDecimalConverter : AbstractTwoWayConverter<sbyte, decimal>
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="SByteToDecimalConverter"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly SByteToDecimalConverter Instance = new SByteToDecimalConverter();
        
        /// <inheritdoc />
        protected override decimal DoConvert(sbyte source) => source;

        /// <inheritdoc />
        protected override sbyte DoConvertBack(decimal source) => (sbyte) source;
    }
}
