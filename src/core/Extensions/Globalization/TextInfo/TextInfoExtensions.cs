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
#if !netstandard
        /// This would be equal to encoding for the <see cref="TextInfo.OEMCodePage"/> for non-invariant cultures.
        /// In case the current <see cref="TextInfo"/> represents a culture-invariant writing system, this method
        /// returns <see cref="Encoding.UTF8"/>.
#else
        /// In .NET Standard this method returns <see cref="Encoding.UTF8"/>.
#endif
        /// </returns>
        /// <seealso cref="Encoding" />
        /// <seealso cref="TextInfo"/>
#if !netstandard
        /// <seealso cref="TextInfo.OEMCodePage" />
#endif
        /// <seealso cref="System.Globalization.CultureInfo.InvariantCulture" />
        /// <exception cref="ArgumentNullException">
        /// <paramref name="textInfo"/> is <c>null</c>.
        /// </exception>
        public static Encoding GetEncoding(this TextInfo textInfo)
        {
#if !netstandard
            return string.IsNullOrEmpty(textInfo.VerifyArgument(nameof(textInfo)).Value.CultureName) ? Encoding.UTF8 : GetOemEncoding(textInfo);
#else
            textInfo.VerifyArgument(nameof(textInfo));
            return Encoding.UTF8;
#endif
        }
    }
}

