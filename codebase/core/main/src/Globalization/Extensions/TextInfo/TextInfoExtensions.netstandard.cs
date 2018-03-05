using System;
using System.Text;

using Axle.Verification;


namespace Axle.Globalization.Extensions.TextInfo
{
    using TextInfo = System.Globalization.TextInfo;

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
        /// In .NET Standard this method returns <see cref="Encoding.UTF8"/>.
        /// </returns>
        /// <seealso cref="Encoding" />
        /// <seealso cref="TextInfo"/>
        /// <seealso cref="System.Globalization.CultureInfo.InvariantCulture" />
        /// <exception cref="ArgumentNullException">
        /// <paramref name="textInfo"/> is <c>null</c>.
        /// </exception>
        public static Encoding GetEncoding(this TextInfo textInfo)
        {
            textInfo.VerifyArgument(nameof(textInfo));
            return Encoding.UTF8;
        }
    }
}

