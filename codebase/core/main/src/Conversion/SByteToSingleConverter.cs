using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="sbyte"/> and <see cref="float"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class SByteToSingleConverter : AbstractTwoWayConverter<sbyte, float>
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="SByteToSingleConverter"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly SByteToSingleConverter Instance = new SByteToSingleConverter();
        
        /// <inheritdoc />
        protected override float DoConvert(sbyte source) => source;

        /// <inheritdoc />
        protected override sbyte DoConvertBack(float source) => (sbyte) source;
    }
}
