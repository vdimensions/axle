using System;
using System.Text;

namespace Axle.Extensions.Globalization.TextInfo
{
    using TextInfo = System.Globalization.TextInfo;

    /// <summary>
    /// A static class providing extension methods for <see cref="System.Globalization.TextInfo"/> instances.
    /// </summary>
    public static class TextInfoExtensions
    {
        public static Encoding GetEncoding(this TextInfo textInfo)
        {
            return string.IsNullOrEmpty(textInfo.CultureName) ? Encoding.UTF8 : GetOemEncoding(textInfo);
        }

        public static Encoding GetOemEncoding(this TextInfo textInfo)
        {
            return Encoding.GetEncoding(textInfo.OEMCodePage);
        }
        public static Encoding GetEbcdicEncoding(this TextInfo textInfo)
        {
            return Encoding.GetEncoding(textInfo.EBCDICCodePage);
        }
        public static Encoding GetAnsiEncoding(this TextInfo textInfo)
        {
            return Encoding.GetEncoding(textInfo.ANSICodePage);
        }
        public static Encoding GetMacEncoding(this TextInfo textInfo)
        {
            return Encoding.GetEncoding(textInfo.MacCodePage);
        }
    }
}

