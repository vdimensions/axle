using System;
using System.Collections.Generic;
using System.Linq;

using Axle.Verification;


namespace Axle.Extensions.String
{
    partial class StringExtensions
    {
        /// <summary>
        /// A polyfill method for string interning.
        /// </summary>
        /// <param name="str">A <see cref="string"/> to be "interned".</param>
        /// <returns>
        /// A reference to <paramref name="str"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="str"/> is <c>null</c>.
        /// </exception>
        public static string Intern(this string str) { return str.VerifyArgument(nameof(str)).Value; }
    }
}