using System.Diagnostics.CodeAnalysis;
using Axle.Text;

namespace Axle.Conversion
{
    /// <summary>
    /// A converter class that can turn a <see cref="string">string</see> instance to a <see cref="CharSequence"/>
    /// instance and vice-versa.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class StringToCharSequenceConverter : AbstractTwoWayConverter<string, CharSequence>
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="StringToCharSequenceConverter"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly StringToCharSequenceConverter Instance = new StringToCharSequenceConverter();
        
        /// <inheritdoc />
        protected override CharSequence DoConvert(string source) => source;

        /// <inheritdoc />
        protected override string DoConvertBack(CharSequence source) => source.ToString();
    }
}