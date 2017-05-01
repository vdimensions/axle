using System;
using System.Collections.Generic;
using System.Linq;

using Axle.Verification;


namespace Axle.Extensions.String
{
    /// <summary>
    /// A static class containing common extension methods to <see cref="String"/> instances.
    /// </summary>
    public static partial class StringExtensions
    {
        #region Contains(...)
        /// <summary>
        /// Determines if the provided by the <paramref name="value"/> parameter <see cref="string"/> is contained 
        /// within the target <see cref="string"/> represented by the <paramref name="str"/> parameter.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance.
        /// </param>
        /// <param name="value">
        /// The <see cref="string"/> to seek. 
        /// </param>
        /// <param name="comparison">
        ///  One of the <see cref="StringComparison"/> enumeration values that specifies the rules for the search. 
        /// </param>
        /// <returns>
        /// A <c>true</c> if the <see cref="string"/> represented by the <paramref name="value"/> parameter is contained
        /// within the target <see cref="string"/> represented by the <paramref name="str"/> parameter; <c>false</c> otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="comparison"/> is not a valid <see cref="StringComparison"/> value.
        /// </exception>
        /// <seealso cref="StringComparison"/>
        /// <seealso cref="string.IndexOf(string, StringComparison)"/>
        public static bool Contains(this string str, string value, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            return str.IndexOf(value, comparison) >= 0;
        }
        /// <summary>
        /// Determines if the provided by the <paramref name="value"/> parameter <see cref="string"/> is contained 
        /// within the target <see cref="string"/> represented by the <paramref name="str"/> parameter.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance.
        /// </param>
        /// <param name="value">
        /// The <see cref="string"/> to seek. 
        /// </param>
        /// <returns>
        /// A <c>true</c> if the <see cref="string"/> represented by the <paramref name="value"/> parameter is contained
        /// within the target <see cref="string"/> represented by the <paramref name="str"/> parameter; <c>false</c> otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <seealso cref="string.IndexOf(string)"/>
        public static bool Contains(this string str, string value)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            return str.IndexOf(value) >= 0;
        }
        /// <summary>
        /// Determines if the provided by the <paramref name="value"/> parameter <see cref="string"/> is contained 
        /// within the target <see cref="string"/> represented by the <paramref name="str"/> parameter.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance.
        /// </param>
        /// <param name="value">
        /// The <see cref="string"/> to seek. 
        /// </param>
        /// <param name="startIndex">
        /// The search starting position. 
        /// </param>
        /// <param name="comparison">
        ///  One of the <see cref="StringComparison"/> enumeration values that specifies the rules for the search. 
        /// </param>
        /// <returns>
        /// A <c>true</c> if the <see cref="string"/> represented by the <paramref name="value"/> parameter is contained
        /// within the target <see cref="string"/> represented by the <paramref name="str"/> parameter; <c>false</c> otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="startIndex"/> is less than zero or greater than the length of <paramref name="str"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="comparison"/> is not a valid <see cref="StringComparison"/> value.
        /// </exception>
        /// <seealso cref="StringComparison"/>
        /// <seealso cref="string.IndexOf(string, int, StringComparison)"/>
        public static bool Contains(this string str, string value, int startIndex, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            return str.IndexOf(value, startIndex, comparison) >= 0;
        }
        /// <summary>
        /// Determines if the provided by the <paramref name="value"/> parameter <see cref="string"/> is contained 
        /// within the target <see cref="string"/> represented by the <paramref name="str"/> parameter.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance.
        /// </param>
        /// <param name="value">
        /// The <see cref="string"/> to seek. 
        /// </param>
        /// <param name="startIndex">
        /// The search starting position. 
        /// </param>
        /// <returns>
        /// A <c>true</c> if the <see cref="string"/> represented by the <paramref name="value"/> parameter is contained
        /// within the <paramref name="str">target string</paramref>; <c>false</c> otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="startIndex"/> is less than zero or greater than the length of <paramref name="str"/>.
        /// </exception>
        /// <seealso cref="string.IndexOf(string, int)"/>
        public static bool Contains(this string str, string value, int startIndex)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            return str.IndexOf(value, startIndex) >= 0;
        }
        /// <summary>
        /// Determines if the provided by the <paramref name="value"/> parameter <see cref="string"/> is contained 
        /// within the target <see cref="string"/> represented by the <paramref name="str"/> parameter.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance.
        /// </param>
        /// <param name="value">
        /// The <see cref="string"/> to seek. 
        /// </param>
        /// <param name="startIndex">
        /// The search starting position. 
        /// </param>
        /// <param name="count">
        /// The number of character positions to examine. 
        /// </param>
        /// <param name="comparison">
        ///  One of the <see cref="StringComparison"/> enumeration values that specifies the rules for the search. 
        /// </param>
        /// <returns>
        /// A <c>true</c> if the <see cref="string"/> represented by the <paramref name="value"/> parameter is contained
        /// within the <paramref name="str">target string</paramref>; <c>false</c> otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="count"/> or <paramref name="startIndex"/> is negative.
        /// <para>-or-</para> 
        /// <paramref name="startIndex"/> is greater than the length of <paramref name="str"/>.
        /// <para>-or-</para>
        /// <paramref name="count"/> is greater than the length of <paramref name="str"/> minus <paramref name="startIndex"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="comparison"/> is not a valid <see cref="StringComparison"/> value.
        /// </exception>
        /// <seealso cref="StringComparison"/>
        /// <seealso cref="string.IndexOf(string, int, int, StringComparison)"/>
        public static bool Contains(this string str, string value, int startIndex, int count, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            return str.IndexOf(value, startIndex, count, comparison) >= 0;
        }
        /// <summary>
        /// Determines if the provided by the <paramref name="value"/> parameter <see cref="string"/> is contained 
        /// within the target <see cref="string"/> represented by the <paramref name="str"/> parameter.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance.
        /// </param>
        /// <param name="value">
        /// The <see cref="string"/> to seek. 
        /// </param>
        /// <param name="startIndex">
        /// The search starting position. 
        /// </param>
        /// <param name="count">
        /// The number of character positions to examine. 
        /// </param>
        /// <returns>
        /// A <c>true</c> if the <see cref="string"/> represented by the <paramref name="value"/> parameter is contained
        /// within the <paramref name="str">target string</paramref>; <c>false</c> otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="count"/> or <paramref name="startIndex"/> is negative.
        /// <para>-or-</para> 
        /// <paramref name="startIndex"/> is greater than the length of <paramref name="str"/>.
        /// <para>-or-</para>
        /// <paramref name="count"/> is greater than the length of <paramref name="str"/> minus <paramref name="startIndex"/>.
        /// </exception>
        /// <seealso cref="string.IndexOf(string, int, int)"/>
        public static bool Contains(this string str, string value, int startIndex, int count)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            return str.IndexOf(value, startIndex, count) >= 0;
        }
        #endregion

        /// <summary>
        /// Checks if the target <see cref="string"/> is an empty string.
        /// </summary>
        /// <param name="str">The target <see cref="string"/>.</param>
        /// <returns>
        /// <c>true</c> if the <paramref name="str">target string</paramref> is an empty string instance; false otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="str"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="string.Empty"/>
        public static bool IsEmpty(this string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            return str.Length == 0;
        }

        /// <summary>
        /// Checks if the target <see cref="string"/> is <c>null</c> or an empty string.
        /// </summary>
        /// <param name="str">The target <see cref="string"/>.</param>
        /// <returns>
        /// <c>true</c> if the <paramref name="str">target string</paramref> is <c>null</c> or an empty string instance; false otherwise.
        /// </returns>
        /// <seealso cref="string.Empty"/>
        /// <seealso cref="string.IsNullOrEmpty(string)"/>
        /// <seealso cref="IsEmpty(string)"/>
        public static bool IsNullOrEmpty(this string str) { return string.IsNullOrEmpty(str); }

        /// <summary>
        /// Creates a <see cref="string"/> using all the chracters from a target string instance, but in a reversed order.
        /// </summary>
        /// <param name="str">The <see cref="string">string instance</see> upon which str extension method is called upon.</param>
        /// <returns>
        /// A new string instance using all the chracters from a target string instance, but in a reversed order.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="str"/> is <c>null</c>.
        /// </exception>
        public static string Reverse(this string str)
        {
            return new string(str.VerifyArgument(nameof(str)).IsNotNull().Value.ToCharArray().Reverse().ToArray());
        }

        #region Split(...)
        /// <summary>
        /// Returns a string array that contains the substrings in the <paramref name="str">target string</paramref> 
        /// that are delimited by elements of a specified Unicode character provided by the <paramref name="separator"/> parameter. 
        /// The <paramref name="options"/> parameter specifies whether to return empty array elements.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/>
        /// </param>
        /// <param name="options">
        /// Use <see cref="StringSplitOptions.RemoveEmptyEntries"/> to omit empty array elements from the array returned; 
        /// or <see cref="StringSplitOptions.None"/> to include empty array elements in the array returned
        /// </param>
        /// <param name="separator">
        /// An Unicode character to act as a delimiter.
        /// </param>
        /// <returns>
        /// A string array that contains the substrings in the <paramref name="str">target string</paramref> 
        /// that are delimited by elements of a specified Unicode character. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="str"/> is <c>null</c>;
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="options"/> is not one of the <see cref="StringSplitOptions"/> values.
        /// </exception>
        /// <seealso cref="string.Split(char[], StringSplitOptions)"/>
        public static string[] Split(this string str, StringSplitOptions options, char separator)
        {
            return str.VerifyArgument(nameof(str)).IsNotNull().Value.Split(new[] { separator }, options);
        }
        /// <summary>
        /// Returns a string array that contains the substrings in the <paramref name="str">target string</paramref> 
        /// that are delimited by elements of a specified Unicode character array provided by the <paramref name="separators"/> parameter. 
        /// The <paramref name="options"/> parameter specifies whether to return empty array elements.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/>
        /// </param>
        /// <param name="options">
        /// Use <see cref="StringSplitOptions.RemoveEmptyEntries"/> to omit empty array elements from the array returned; 
        /// or <see cref="StringSplitOptions.None"/> to include empty array elements in the array returned
        /// </param>
        /// <param name="separators">
        /// An array of Unicode characters to act as delimiters, an empty array or <c>null</c>.
        /// </param>
        /// <returns>
        /// A string array that contains the substrings in the <paramref name="str">target string</paramref> 
        /// that are delimited by elements of a specified Unicode character. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="str"/> is <c>null</c>;
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="options"/> is not one of the <see cref="StringSplitOptions"/> values.
        /// </exception>
        /// <seealso cref="string.Split(char[], StringSplitOptions)"/>
        public static string[] Split(this string str, StringSplitOptions options, params char[] separators)
        {
            return str.VerifyArgument(nameof(str)).IsNotNull().Value.Split(separators, options);
        }
        /// <summary>
        /// Returns a string array that contains the substrings in the <paramref name="str">target string</paramref> 
        /// that are delimited by elements of a specified Unicode character array provided by the <paramref name="separators"/> parameter. 
        /// The <paramref name="options"/> parameter specifies whether to return empty array elements.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/>
        /// </param>
        /// <param name="options">
        /// Use <see cref="StringSplitOptions.RemoveEmptyEntries"/> to omit empty array elements from the array returned; 
        /// or <see cref="StringSplitOptions.None"/> to include empty array elements in the array returned
        /// </param>
        /// <param name="count">
        /// The maximum number of substrings to return. 
        /// </param>
        /// <param name="separators">
        /// An array of Unicode characters to act as delimiters, an empty array or <c>null</c>.
        /// </param>
        /// <returns>
        /// A string array that contains the substrings in the <paramref name="str">target string</paramref> 
        /// that are delimited by elements of a specified Unicode character. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="str"/> is <c>null</c>;
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="count"/> is negative.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="options"/> is not one of the <see cref="StringSplitOptions"/> values.
        /// </exception>
        /// <seealso cref="string.Split(char[], int, StringSplitOptions)"/>
        public static string[] Split(this string str, StringSplitOptions options, int count, params char[] separators)
        {
            return str.VerifyArgument(nameof(str)).IsNotNull().Value.Split(separators, count, options);
        }
        /// <summary>
        /// Returns a string array that contains the substrings in the <paramref name="str">target string</paramref> 
        /// that are delimited by elements of a specified array of strings provided by the <paramref name="separators"/> parameter. 
        /// The <paramref name="options"/> parameter specifies whether to return empty array elements.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/>
        /// </param>
        /// <param name="options">
        /// Use <see cref="StringSplitOptions.RemoveEmptyEntries"/> to omit empty array elements from the array returned; 
        /// or <see cref="StringSplitOptions.None"/> to include empty array elements in the array returned
        /// </param>
        /// <param name="separators">
        /// An array of strings to act as delimiters, an empty array or <c>null</c>.
        /// </param>
        /// <returns>
        /// A string array that contains the substrings in the <paramref name="str">target string</paramref> 
        /// that are delimited by elements of a specified array of strings. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="str"/> is <c>null</c>;
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="options"/> is not one of the <see cref="StringSplitOptions"/> values.
        /// </exception>
        /// <seealso cref="string.Split(char[], int, StringSplitOptions)"/>
        public static string[] Split(this string str, StringSplitOptions options, params string[] separators)
        {
            return str.VerifyArgument(nameof(str)).IsNotNull().Value.Split(separators, options);
        }
        /// <summary>
        /// Returns a string array that contains the substrings in the <paramref name="str">target string</paramref> 
        /// that are delimited by elements of a specified array of strings provided by the <paramref name="separators"/> parameter. 
        /// The <paramref name="options"/> parameter specifies whether to return empty array elements.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/>
        /// </param>
        /// <param name="options">
        /// Use <see cref="StringSplitOptions.RemoveEmptyEntries"/> to omit empty array elements from the array returned; 
        /// or <see cref="StringSplitOptions.None"/> to include empty array elements in the array returned
        /// </param>
        /// <param name="count">
        /// The maximum number of substrings to return. 
        /// </param>
        /// <param name="separators">
        /// An array of strings to act as delimiters, an empty array or <c>null</c>.
        /// </param>
        /// <returns>
        /// A string array that contains the substrings in the <paramref name="str">target string</paramref> 
        /// that are delimited by elements of a specified array of strings. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="str"/> is <c>null</c>;
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="count"/> is negative.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="options"/> is not one of the <see cref="StringSplitOptions"/> values.
        /// </exception>
        /// <seealso cref="string.Split(char[], int, StringSplitOptions)"/>
        public static string[] Split(this string str, StringSplitOptions options, int count, params string[] separators)
        {
            return str.VerifyArgument(nameof(str)).IsNotNull().Value.Split(separators, count, options);
        }
        /// <summary>
        /// Returns a string array that contains the substrings in the <paramref name="str">target string</paramref> 
        /// that are delimited by elements of a specified array of strings provided by the <paramref name="separators"/> parameter. 
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/>
        /// </param>
        /// <param name="separators">
        /// An array of strings to act as delimiters, an empty array or <c>null</c>.
        /// </param>
        /// <returns>
        /// A string array that contains the substrings in the <paramref name="str">target string</paramref> 
        /// that are delimited by elements of a specified array of strings. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="str"/> is <c>null</c>;
        /// </exception>
        /// <seealso cref="string.Split(char[], StringSplitOptions)"/>
        public static string[] Split(this string str, params string[] separators)
        {
            return str.VerifyArgument(nameof(str)).IsNotNull().Value.Split(separators, StringSplitOptions.None);
        }
        #endregion

        private static string CutFromIndex(
            Func<string, string, StringComparison, int> searchFunc,
            string stringToTrim,
            string stringToSearch,
            StringComparison comparison,
            bool trimEnd)
        {
            var index = searchFunc(stringToTrim, stringToSearch, comparison);
            if (index < 0)
            {
                return stringToTrim;
            }
            return trimEnd ? stringToTrim.Substring(0, index) : stringToTrim.Substring(index + stringToSearch.Length);
        }
        private static string CutFromIndex(Func<string, char, int> searchFunc, string stringToTrim, char charToSearch, bool trimEnd)
        {
            var index = searchFunc(stringToTrim, charToSearch);
            if (index < 0)
            {
                return stringToTrim;
            }
            return trimEnd ? stringToTrim.Substring(0, index) : stringToTrim.Substring(index);
        }

        #region TakeBeforeFirst(...)
        public static string TakeBeforeFirst(this string str, string stringToSearch, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (stringToSearch == null)
            {
                throw new ArgumentNullException(nameof(stringToSearch));
            }
            return CutFromIndex((x, y, z) => x.IndexOf(y, 0, z), str, stringToSearch, comparison, true);
        }
        public static string TakeBeforeFirst(this string str, string stringToSearch, int startIndex, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (stringToSearch == null)
            {
                throw new ArgumentNullException(nameof(stringToSearch));
            }
            return CutFromIndex((x, y, z) => x.IndexOf(y, startIndex, z), str, stringToSearch, comparison, true);
        }

        public static string TakeBeforeFirst(this string str, string stringToSearch)
        {
            return TakeBeforeFirst(str, stringToSearch, StringComparison.CurrentCulture);
        }
        public static string TakeBeforeFirst(this string str, string stringToSearch, int startIndex)
        {
            return TakeBeforeFirst(str, stringToSearch, startIndex, StringComparison.CurrentCulture);
        }
        public static string TakeBeforeFirst(this string str, char charToSearch)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            return CutFromIndex((x, y) => x.IndexOf(y, 0), str, charToSearch, true);
        }
        public static string TakeBeforeFirst(this string str, char charToSearch, int startIndex)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            return CutFromIndex((x, y) => x.IndexOf(x, y, startIndex), str, charToSearch, true);
        }
        #endregion

        #region TakeBeforeLast(...)
        public static string TakeBeforeLast(this string str, string stringToSearch, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (stringToSearch == null)
            {
                throw new ArgumentNullException(nameof(stringToSearch));
            }
            return CutFromIndex((x, y, z) => x.LastIndexOf(y, z), str, stringToSearch, comparison, true);
        }
        public static string TakeBeforeLast(this string str, string stringToSearch, int startIndex, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (stringToSearch == null)
            {
                throw new ArgumentNullException(nameof(stringToSearch));
            }
            return CutFromIndex((x, y, z) => x.LastIndexOf(y, startIndex, z), str, stringToSearch, comparison, true);
        }

        public static string TakeBeforeLast(this string str, string stringToSearch)
        {
            return TakeBeforeLast(str, stringToSearch, StringComparison.CurrentCulture);
        }
        public static string TakeBeforeLast(this string str, string stringToSearch, int startIndex)
        {
            return TakeBeforeLast(str, stringToSearch, startIndex, StringComparison.CurrentCulture);
        }
        public static string TakeBeforeLast(this string str, char charToSearch)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            return CutFromIndex((x, y) => x.LastIndexOf(y), str, charToSearch, true);
        }
        public static string TakeBeforeLast(this string str, char charToSearch, int startIndex)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            return CutFromIndex((x, y) => x.LastIndexOf(x, y, startIndex), str, charToSearch, true);
        }
        #endregion

        #region TakeAfterFirst(...)
        public static string TakeAfterFirst(this string str, string stringToSearch, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (stringToSearch == null)
            {
                throw new ArgumentNullException(nameof(stringToSearch));
            }
            return CutFromIndex((x, y, z) => x.IndexOf(y, 0, z), str, stringToSearch, comparison, false);
        }
        public static string TakeAfterFirst(this string str, string stringToSearch, int startIndex, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (stringToSearch == null)
            {
                throw new ArgumentNullException(nameof(stringToSearch));
            }
            return CutFromIndex((x, y, z) => x.IndexOf(y, startIndex, z), str, stringToSearch, comparison, false);
        }
        public static string TakeAfterFirst(this string str, string stringToSearch)
        {
            return TakeAfterFirst(str, stringToSearch, StringComparison.CurrentCulture);
        }
        public static string TakeAfterFirst(this string str, int startIndex, string stringToSearch)
        {
            return TakeAfterFirst(str, stringToSearch, startIndex, StringComparison.CurrentCulture);
        }
        public static string TakeAfterFirst(this string str, char charToSearch)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            return CutFromIndex((x, y) => x.IndexOf(y, 0), str, charToSearch, false);
        }
        public static string TakeAfterFirst(this string str, char charToSearch, int startIndex)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            return CutFromIndex((x, y) => x.IndexOf(x, y, startIndex), str, charToSearch, false);
        }
        #endregion

        #region TakeAfterLast(...)
        public static string TakeAfterLast(this string str, string stringToSearch, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (stringToSearch == null)
            {
                throw new ArgumentNullException(nameof(stringToSearch));
            }
            return CutFromIndex((x, y, z) => x.LastIndexOf(y, z), str, stringToSearch, comparison, false);
        }
        public static string TakeAfterLast(this string str, string stringToSearch, int startIndex, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (stringToSearch == null)
            {
                throw new ArgumentNullException(nameof(stringToSearch));
            }
            return CutFromIndex((x, y, z) => x.LastIndexOf(y, startIndex, z), str, stringToSearch, comparison, false);
        }
        public static string TakeAfterLast(this string str, string stringToSearch)
        {
            return TakeAfterLast(str, stringToSearch, StringComparison.CurrentCulture);
        }
        public static string TakeAfterLast(this string str, int startIndex, string stringToSearch)
        {
            return TakeAfterLast(str, stringToSearch, startIndex, StringComparison.CurrentCulture);
        }
        public static string TakeAfterLast(this string str, char charToSearch)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            return CutFromIndex((x, y) => x.LastIndexOf(y), str, charToSearch, false);
        }
        public static string TakeAfterLast(this string str, char charToSearch, int startIndex)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            return CutFromIndex((x, y) => x.LastIndexOf(x, y, startIndex), str, charToSearch, false);
        }
        #endregion

        #region TrimStart(...)
        public static string CutStart(this string str, string stringToSearch, StringComparison comparison)
        {
            return str.VerifyArgument("str").IsNotNull().Value.StartsWith(stringToSearch, comparison)
                ? str.TakeAfterFirst(stringToSearch, 0, comparison)
                : str;
        }
        public static string CutStart(this string str, string stringToSearch) { return CutStart(str, stringToSearch, StringComparison.CurrentCulture); }
        public static string CutStart(this string str, char charToSearch)
        {
            return str.VerifyArgument("str").IsNotNull().Value.Length > 0 && str[0] == charToSearch
                ? str.Substring(1)
                : str;
        }
        #endregion

        #region TrimEnd(...)
        public static string CutEnd(this string str, string stringToSearch, StringComparison comparison)
        {
            return str.VerifyArgument("str").IsNotNull().Value.EndsWith(stringToSearch, comparison)
                ? str.TakeBeforeLast(stringToSearch, comparison)
                : str;
        }
        public static string CutEnd(this string str, string stringToSearch) { return CutEnd(str, stringToSearch, StringComparison.CurrentCulture); }
        public static string CutEnd(this string str, char charToSearch)
        {
            return str.VerifyArgument("str").IsNotNull().Value.Length > 0 && str[str.Length - 1] == charToSearch
                ? str.Substring(0, str.Length - 1)
                : str;
        }
        #endregion

        private static string JoinInternal(string[] values, string separator) { return string.Join(separator, values); }
        private static string JoinInternal(IEnumerable<string> values, string separator)
        {
            return string.Join(separator, values.ToArray());
        }

        public static string Join(this string separator, IEnumerable<string> values) { return JoinInternal(values, separator); }
        public static string Join(this char separator, IEnumerable<string> values) { return JoinInternal(values, separator.ToString()); }
        public static string Join(this string separator, params string[] values) { return JoinInternal(values, separator); }
        public static string Join(this char separator, params string[] values) { return JoinInternal(values, separator.ToString()); }

        [Obsolete] public static string Join(this IEnumerable<string> @this, string separator) { return JoinInternal(@this, separator); }
        [Obsolete] public static string Join(this IEnumerable<string> @this, char separator) { return JoinInternal(@this, separator.ToString()); }
        [Obsolete] public static string Join(this IEnumerable<string> @this) { return JoinInternal(@this, string.Empty); }
        [Obsolete] public static string Join(this string[] @this, string separator) { return JoinInternal(@this, separator); }
        [Obsolete] public static string Join(this string[] @this, char separator) { return JoinInternal(@this, separator.ToString()); }
        [Obsolete] public static string Join(this string[] @this) { return JoinInternal(@this, string.Empty); }
    }
}