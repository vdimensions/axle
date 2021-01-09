#if NETSTANDARD || NET20_OR_NEWER || UNITY_2018_1_OR_NEWER
using System;
using System.Text;

using Axle.Verification;


namespace Axle.Globalization.Extensions.TextInfo
{
    using TextInfo = System.Globalization.TextInfo;

    /// <summary>
    /// A <see langword="static"/> class providing extension methods for <see cref="System.Globalization.TextInfo"/>
    /// instances.
    /// </summary>
    public static class TextInfoExtensions
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
        /// <summary>
        /// Gets the encoding for the original equipment manufacturer OEM code page of the writing system represented by
        /// the current <see cref="TextInfo"/>. 
        /// </summary>
        /// <param name="textInfo">
        /// The <see cref="TextInfo"/> instance to get the encoding for. 
        /// </param>
        /// <returns>
        /// A reference to the encoding for the OEM code page of the writing system represented by the current
        /// <see cref="TextInfo"/>. 
        /// </returns>
        /// <seealso cref="TextInfo.OEMCodePage"/>
        public static Encoding GetOemEncoding(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            TextInfo textInfo)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(textInfo, nameof(textInfo)));
            return Encoding.GetEncoding(textInfo.OEMCodePage);
        }

        /// <summary>
        /// Gets the encoding for the Extended Binary Coded Decimal Interchange Code (EBCDIC) codepage of the writing
        /// system represented by the current <see cref="TextInfo"/>. 
        /// </summary>
        /// <param name="textInfo">
        /// The <see cref="TextInfo"/> instance to get the encoding for. 
        /// </param>
        /// <returns>
        /// A reference to the encoding for the EBCDIC codepage of the writing system represented by the current
        /// <see cref="TextInfo"/>. 
        /// </returns>
        /// <seealso cref="TextInfo.EBCDICCodePage"/>
        public static Encoding GetEbcdicEncoding(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            TextInfo textInfo)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(textInfo, nameof(textInfo)));
            return Encoding.GetEncoding(textInfo.EBCDICCodePage);
        }

        /// <summary>
        /// Gets the encoding for the American National Standards Institute (ANSI) codepage of the writing system
        /// represented by the current <see cref="TextInfo"/>. 
        /// </summary>
        /// <param name="textInfo">
        /// The <see cref="TextInfo"/> instance to get the encoding for. 
        /// </param>
        /// <returns>
        /// A reference to the encoding for the American National Standards Institute (ANSI) codepage of the writing
        /// system represented by the current <see cref="TextInfo"/>. 
        /// </returns>
        /// <seealso cref="TextInfo.ANSICodePage"/>
        public static Encoding GetAnsiEncoding(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            TextInfo textInfo)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(textInfo, nameof(textInfo)));
            return Encoding.GetEncoding(textInfo.ANSICodePage);
        }

        /// <summary>
        /// Gets the encoding for the Macintosh codepage of the writing system represented by the current
        /// <see cref="TextInfo"/>. 
        /// </summary>
        /// <param name="textInfo">
        /// The <see cref="TextInfo"/> instance to get the encoding for. 
        /// </param>
        /// <returns>
        /// A reference to the encoding for the Macintosh codepage of the writing system represented by the current
        /// <see cref="TextInfo"/>. 
        /// </returns>
        /// <seealso cref="TextInfo.MacCodePage"/>
        public static Encoding GetMacEncoding(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            TextInfo textInfo)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(textInfo, nameof(textInfo)));
            return Encoding.GetEncoding(textInfo.MacCodePage);
        }
        #endif

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
        /// <summary>
        /// Gets the default encoding for the writing system represented by the current <see cref="TextInfo"/>. 
        /// This would be equal to encoding for the <see cref="TextInfo.OEMCodePage"/> for non-invariant cultures.
        /// In case the current <see cref="TextInfo"/> represents a culture-invariant writing system, this method
        /// returns <see cref="Encoding.UTF8"/>. 
        /// </summary>
        /// <param name="textInfo">
        /// The <see cref="TextInfo"/> instance to get the encoding for.
        /// </param>
        /// <returns>
        /// A reference to the default encoding for the writing system represented by the current <see cref="TextInfo"/>. 
        /// This would be equal to encoding for the <see cref="TextInfo.OEMCodePage"/> for non-invariant cultures.
        /// In case the current <see cref="TextInfo"/> represents a culture-invariant writing system, this method
        /// returns <see cref="Encoding.UTF8"/>.
        /// </returns>
        /// <seealso cref="Encoding" />
        /// <seealso cref="TextInfo"/>
        /// <seealso cref="TextInfo.OEMCodePage" />
        /// <seealso cref="System.Globalization.CultureInfo.InvariantCulture" />
        /// <exception cref="ArgumentNullException">
        /// <paramref name="textInfo"/> is <c>null</c>.
        /// </exception>
        public static Encoding GetEncoding(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            TextInfo textInfo)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(textInfo, nameof(textInfo)));
            return string.IsNullOrEmpty(textInfo.CultureName) ? Encoding.UTF8 : GetOemEncoding(textInfo);
        }
        #else
        /// <summary>
        /// Gets the default encoding for the writing system represented by the current <see cref="TextInfo"/>. 
        /// In case the current <see cref="TextInfo"/> represents a culture-invariant writing system, this method
        /// returns <see cref="Encoding.UTF8"/>. 
        /// </summary>
        /// <param name="textInfo">
        /// The <see cref="TextInfo"/> instance to get the encoding for.
        /// </param>
        /// <returns>
        /// A reference to the default encoding for the writing system represented by the current
        /// <see cref="TextInfo"/>. 
        /// In .NET Standard this method returns <see cref="Encoding.UTF8"/>.
        /// </returns>
        /// <seealso cref="Encoding" />
        /// <seealso cref="TextInfo"/>
        /// <seealso cref="System.Globalization.CultureInfo.InvariantCulture" />
        /// <exception cref="ArgumentNullException">
        /// <paramref name="textInfo"/> is <c><see langword="null"/></c>.
        /// </exception>
        public static Encoding GetEncoding(this TextInfo textInfo)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(textInfo, nameof(textInfo)));
            return Encoding.UTF8;
        }
        #endif
    }
}
#endif