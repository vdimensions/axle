using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="float"/> and <see cref="double"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class SingleToDoubleConverter : AbstractTwoWayConverter<float, double>
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="SingleToDoubleConverter"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly SingleToDoubleConverter Instance = new SingleToDoubleConverter();
        
        /// <inheritdoc />
        protected override double DoConvert(float source) => source;

        /// <inheritdoc />
        protected override float DoConvertBack(double source) => (float) source;
    }
}
