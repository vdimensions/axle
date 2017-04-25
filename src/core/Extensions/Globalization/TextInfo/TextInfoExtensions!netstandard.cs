using System;
using System.Text;

using Axle.Verification;


namespace Axle.Extensions.Globalization.TextInfo
{
    using TextInfo = System.Globalization.TextInfo;

    /// <summary>
    /// A static class providing extension methods for <see cref="System.Globalization.TextInfo"/> instances.
    /// </summary>
    public static partial class TextInfoExtensions
    {
        /// <summary>
        /// Gets the encoding for the OEM code page of the writing system represented by the current <see cref="TextInfo"/>. 
        /// </summary>
        /// <param name="textInfo">
        /// The <see cref="TextInfo"/> instance to get the encoding for. 
        /// </param>
        /// <returns>
        /// A reference to the encoding for the OEM code page of the writing system represented by the current <see cref="TextInfo"/>. 
        /// </returns>
        /// <seealso cref="TextInfo.OEMCodePage"/>
        public static Encoding GetOemEncoding(this TextInfo textInfo)
        {
            return Encoding.GetEncoding(textInfo.VerifyArgument(nameof(textInfo)).Value.OEMCodePage);
        }

        /// <summary>
        /// Gets the encoding for the EBCDIC codepage of the writing system represented by the current <see cref="TextInfo"/>. 
        /// </summary>
        /// <param name="textInfo">
        /// The <see cref="TextInfo"/> instance to get the encoding for. 
        /// </param>
        /// <returns>
        /// A reference to the encoding for the EBCDIC codepage of the writing system represented by the current <see cref="TextInfo"/>. 
        /// </returns>
        /// <seealso cref="TextInfo.EBCDICCodePage"/>
        public static Encoding GetEbcdicEncoding(this TextInfo textInfo)
        {
            return Encoding.GetEncoding(textInfo.VerifyArgument(nameof(textInfo)).Value.EBCDICCodePage);
        }

        /// <summary>
        /// Gets the encoding for the ANSI codepage of the writing system represented by the current <see cref="TextInfo"/>. 
        /// </summary>
        /// <param name="textInfo">
        /// The <see cref="TextInfo"/> instance to get the encoding for. 
        /// </param>
        /// <returns>
        /// A reference to the encoding for the ANSI codepage of the writing system represented by the current <see cref="TextInfo"/>. 
        /// </returns>
        /// <seealso cref="TextInfo.ANSICodePage"/>
        public static Encoding GetAnsiEncoding(this TextInfo textInfo)
        {
            return Encoding.GetEncoding(textInfo.VerifyArgument(nameof(textInfo)).Value.ANSICodePage);
        }

        /// <summary>
        /// Gets the encoding for the Mac codepage of the writing system represented by the current <see cref="TextInfo"/>. 
        /// </summary>
        /// <param name="textInfo">
        /// The <see cref="TextInfo"/> instance to get the encoding for. 
        /// </param>
        /// <returns>
        /// A reference to the encoding for the Mac codepage of the writing system represented by the current <see cref="TextInfo"/>. 
        /// </returns>
        /// <seealso cref="TextInfo.MacCodePage"/>
        public static Encoding GetMacEncoding(this TextInfo textInfo)
        {
            return Encoding.GetEncoding(textInfo.VerifyArgument(nameof(textInfo)).Value.MacCodePage);
        }
    }
}

