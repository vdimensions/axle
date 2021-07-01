using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="sbyte"/> and <see cref="double"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class SByteToDoubleConverter : AbstractTwoWayConverter<sbyte, double>
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="SByteToDoubleConverter"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly SByteToDoubleConverter Instance = new SByteToDoubleConverter();

        /// <inheritdoc />
        protected override double DoConvert(sbyte source) => source;

        /// <inheritdoc />
        protected override sbyte DoConvertBack(double source) => (sbyte) source;
    }
}
