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
        protected override CharSequence DoConvert(string source) => source;
        
        protected override string DoConvertBack(CharSequence source) => source.ToString();
    }
}