using System;
using System.Collections.Generic;
using System.Linq;

using Axle.Verification;


namespace Axle.Extensions.String
{
    using StringComparison = System.StringComparison;

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
        #endregion Contains(...)

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
        /// Creates a <see cref="string"/> using all the characters from a target string instance, but in a reversed order.
        /// </summary>
        /// <param name="str">The <see cref="string">string instance</see> upon which the extension method is called upon.</param>
        /// <returns>
        /// A new string instance using all the characters from a target string instance, but in a reversed order.
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
        #endregion Split(...)

        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
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
                return string.Empty;// stringToTrim;
            }
            return trimEnd ? stringToTrim.Substring(0, index) : stringToTrim.Substring(index + stringToSearch.Length);
        }
        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        private static string CutFromIndex(Func<string, char, int> searchFunc, string stringToTrim, char charToSearch, bool trimEnd)
        {
            var index = searchFunc(stringToTrim, charToSearch);
            if (index < 0)
            {
                return string.Empty;//stringToTrim;
            }
            return trimEnd ? stringToTrim.Substring(0, index) : stringToTrim.Substring(index);
        }

        #region TakeBeforeFirst(...)
        /// <summary>
        /// Takes the part of a string that precedes the first occurrence of a given <paramref name="value"/>.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The <see cref="string"/> value to cut the string by.
        /// </param>
        /// <param name="startIndex">
        /// The index to start the searching from.
        /// </param>
        /// <param name="comparison">
        /// One of the <see cref="StringComparison"/> values, determining the string comparison method to be used for searching.
        /// </param>
        /// <returns>
        /// The part of the original string that precedes the first occurrence of a given <paramref name="value"/>, or 
        /// the original string if the <paramref name="value"/> was not found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="IndexOutOfRangeException">
        /// <paramref name="startIndex"/> specifies a position that is greater than the length of the string <paramref name="str"/>, 
        /// or is less than zero.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="comparison"/> is not a valid <see cref="StringComparison"/> value.
        /// </exception>
        public static string TakeBeforeFirst(this string str, string value, int startIndex, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            var candidate = CutFromIndex((x, y, z) => x.IndexOf(y, startIndex, z), str, value, comparison, true);
            return candidate.Length > 0 ? candidate : str;
        }

        /// <summary>
        /// Takes the part of a string that precedes the first occurrence of a given <paramref name="value"/>.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The <see cref="string"/> value to cut the string by.
        /// </param>
        /// <param name="comparison">
        /// One of the <see cref="StringComparison"/> values, determining the string comparison method to be used for searching.
        /// </param>
        /// <returns>
        /// The part of the original string that precedes the first occurrence of a given <paramref name="value"/>, or 
        /// the original string if the <paramref name="value"/> was not found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="comparison"/> is not a valid <see cref="StringComparison"/> value.
        /// </exception>
        public static string TakeBeforeFirst(this string str, string value, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            var candidate = CutFromIndex((x, y, z) => x.IndexOf(y, 0, z), str, value, comparison, true);
            return candidate.Length > 0 ? candidate : str;
        }

        /// <summary>
        /// Takes the part of a string that precedes the first occurrence of a given <paramref name="value"/>.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The <see cref="string"/> value to cut the string by.
        /// </param>
        /// <param name="startIndex">
        /// The index to start the searching from.
        /// </param>
        /// <returns>
        /// The part of the original string that precedes the first occurrence of a given <paramref name="value"/>, or 
        /// the original string if the <paramref name="value"/> was not found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="IndexOutOfRangeException">
        /// <paramref name="startIndex"/> specifies a position that is greater than the length of the string <paramref name="str"/>, 
        /// or is less than zero.
        /// </exception>
        public static string TakeBeforeFirst(this string str, string value, int startIndex)
        {
            return TakeBeforeFirst(str, value, startIndex, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Takes the part of a string that precedes the first occurrence of a given <paramref name="value"/>.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The <see cref="string"/> value to cut the string by.
        /// </param>
        /// <returns>
        /// The part of the original string that precedes the first occurrence of a given <paramref name="value"/>, or 
        /// the original string if the <paramref name="value"/> was not found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        public static string TakeBeforeFirst(this string str, string value)
        {
            return TakeBeforeFirst(str, value, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Takes the part of a string that precedes the first occurrence of a given <paramref name="value"/>.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The <see cref="string"/> value to cut the string by.
        /// </param>
        /// <param name="startIndex">
        /// The index to start the searching from.
        /// </param>
        /// <returns>
        /// The part of the original string that precedes the first occurrence of a given <paramref name="value"/>, or 
        /// the original string if the <paramref name="value"/> was not found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="IndexOutOfRangeException">
        /// <paramref name="startIndex"/> specifies a position that is greater than the length of the string <paramref name="str"/>, 
        /// or is less than zero.
        /// </exception>
        public static string TakeBeforeFirst(this string str, char value, int startIndex)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            var candidate = CutFromIndex((x, y) => x.IndexOf(x, y, startIndex), str, value, true);
            return candidate.Length > 0 ? candidate : str;
        }

        /// <summary>
        /// Takes the part of a string that precedes the first occurrence of a given <paramref name="value"/>.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The <see cref="string"/> value to cut the string by.
        /// </param>
        /// <returns>
        /// The part of the original string that precedes the first occurrence of a given <paramref name="value"/>, or 
        /// the original string if the <paramref name="value"/> was not found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        public static string TakeBeforeFirst(this string str, char value)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            var candidate = CutFromIndex((x, y) => x.IndexOf(y, 0), str, value, true);
            return candidate.Length > 0 ? candidate : str;
        }
        #endregion TakeBeforeFirst(...)

        #region TakeBeforeLast(...)
        /// <summary>
        /// Takes the part of a string that precedes the last occurrence of a given <paramref name="value"/>.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The <see cref="string"/> value to cut the string by.
        /// </param>
        /// <param name="startIndex">
        /// The index to start the searching from.
        /// </param>
        /// <param name="comparison">
        /// One of the <see cref="StringComparison"/> values, determining the string comparison method to be used for searching.
        /// </param>
        /// <returns>
        /// The part of the original string that precedes the last occurrence of a given <paramref name="value"/>, or 
        /// the original string if the <paramref name="value"/> was not found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="IndexOutOfRangeException">
        /// <paramref name="startIndex"/> specifies a position that is greater than the length of the string <paramref name="str"/>, 
        /// or is less than zero.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="comparison"/> is not a valid <see cref="StringComparison"/> value.
        /// </exception>
        public static string TakeBeforeLast(this string str, string value, int startIndex, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            var candidate = CutFromIndex((x, y, z) => x.LastIndexOf(y, startIndex, z), str, value, comparison, true);
            return candidate.Length > 0 ? candidate : str;
        }

        /// <summary>
        /// Takes the part of a string that precedes the last occurrence of a given <paramref name="value"/>.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The <see cref="string"/> value to cut the string by.
        /// </param>
        /// <param name="comparison">
        /// One of the <see cref="StringComparison"/> values, determining the string comparison method to be used for searching.
        /// </param>
        /// <returns>
        /// The part of the original string that precedes the last occurrence of a given <paramref name="value"/>, or 
        /// the original string if the <paramref name="value"/> was not found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="comparison"/> is not a valid <see cref="StringComparison"/> value.
        /// </exception>
        public static string TakeBeforeLast(this string str, string value, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            var candidate = CutFromIndex((x, y, z) => x.LastIndexOf(y, z), str, value, comparison, true);
            return candidate.Length > 0 ? candidate : str;
        }

        /// <summary>
        /// Takes the part of a string that precedes the last occurrence of a given <paramref name="value"/>.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The <see cref="string"/> value to cut the string by.
        /// </param>
        /// <param name="startIndex">
        /// The index to start the searching from.
        /// </param>
        /// <returns>
        /// The part of the original string that precedes the last occurrence of a given <paramref name="value"/>, or 
        /// the original string if the <paramref name="value"/> was not found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="IndexOutOfRangeException">
        /// <paramref name="startIndex"/> specifies a position that is greater than the length of the string <paramref name="str"/>, 
        /// or is less than zero.
        /// </exception>
        public static string TakeBeforeLast(this string str, string value, int startIndex)
        {
            return TakeBeforeLast(str, value, startIndex, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Takes the part of a string that precedes the last occurrence of a given <paramref name="value"/>.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The <see cref="string"/> value to cut the string by.
        /// </param>
        /// <returns>
        /// The part of the original string that precedes the last occurrence of a given <paramref name="value"/>, or 
        /// the original string if the <paramref name="value"/> was not found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        public static string TakeBeforeLast(this string str, string value)
        {
            return TakeBeforeLast(str, value, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Takes the part of a string that precedes the last occurrence of a given <paramref name="value"/>.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The <see cref="char"/> value to cut the string by.
        /// </param>
        /// <param name="startIndex">
        /// The index to start the searching from.
        /// </param>
        /// <returns>
        /// The part of the original string that precedes the last occurrence of a given <paramref name="value"/>, or 
        /// the original string if the <paramref name="value"/> was not found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="IndexOutOfRangeException">
        /// <paramref name="startIndex"/> specifies a position that is greater than the length of the string <paramref name="str"/>, 
        /// or is less than zero.
        /// </exception>
        public static string TakeBeforeLast(this string str, char value, int startIndex)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            var candidate = CutFromIndex((x, y) => x.LastIndexOf(x, y, startIndex), str, value, true);
            return candidate.Length > 0 ? candidate : str;
        }

        /// <summary>
        /// Takes the part of a string that precedes the last occurrence of a given <paramref name="value"/>.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The <see cref="char"/> value to cut the string by.
        /// </param>
        /// <returns>
        /// The part of the original string that precedes the last occurrence of a given <paramref name="value"/>, or 
        /// the original string if the <paramref name="value"/> was not found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        public static string TakeBeforeLast(this string str, char value)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            var candidate = CutFromIndex((x, y) => x.LastIndexOf(y), str, value, true);
            return candidate.Length > 0 ? candidate : str;
        }
        #endregion TakeBeforeLast(...)

        #region TakeAfterFirst(...)
        /// <summary>
        /// Takes the part of a string that follows the first occurrence of a given <paramref name="value"/>.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The <see cref="string"/> value to cut the string by.
        /// </param>
        /// <param name="startIndex">
        /// The index to start the searching from.
        /// </param>
        /// <param name="comparison">
        /// One of the <see cref="StringComparison"/> values, determining the string comparison method to be used for searching.
        /// </param>
        /// <returns>
        /// The part of the original string that follows the first occurrence of a given <paramref name="value"/>, or 
        /// <see cref="System.String.Empty">an empty string</see> if the <paramref name="value"/> was not found in the string.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="IndexOutOfRangeException">
        /// <paramref name="startIndex"/> specifies a position that is greater than the length of the string <paramref name="str"/>, 
        /// or is less than zero.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="comparison"/> is not a valid <see cref="StringComparison"/> value.
        /// </exception>
        public static string TakeAfterFirst(this string str, string value, int startIndex, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            return CutFromIndex((x, y, z) => x.IndexOf(y, startIndex, z), str, value, comparison, false);
        }

        /// <summary>
        /// Takes the part of a string that follows the first occurrence of a given <paramref name="value"/>.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The <see cref="string"/> value to cut the string by.
        /// </param>
        /// <param name="comparison">
        /// One of the <see cref="StringComparison"/> values, determining the string comparison method to be used for searching.
        /// </param>
        /// <returns>
        /// The part of the original string that follows the first occurrence of a given <paramref name="value"/>, or 
        /// <see cref="System.String.Empty">an empty string</see> if the <paramref name="value"/> was not found in the string.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="comparison"/> is not a valid <see cref="StringComparison"/> value.
        /// </exception>
        public static string TakeAfterFirst(this string str, string value, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            return CutFromIndex((x, y, z) => x.IndexOf(y, 0, z), str, value, comparison, false);
        }

        /// <summary>
        /// Takes the part of a string that follows the first occurrence of a given <paramref name="value"/>.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The <see cref="string"/> value to cut the string by.
        /// </param>
        /// <returns>
        /// The part of the original string that follows the first occurrence of a given <paramref name="value"/>, or 
        /// <see cref="System.String.Empty">an empty string</see> if the <paramref name="value"/> was not found in the string.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        public static string TakeAfterFirst(this string str, string value)
        {
            return TakeAfterFirst(str, value, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Takes the part of a string that follows the first occurrence of a given <paramref name="value"/>.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The <see cref="string"/> value to cut the string by.
        /// </param>
        /// <param name="startIndex">
        /// The index to start the searching from.
        /// </param>
        /// <returns>
        /// The part of the original string that follows the first occurrence of a given <paramref name="value"/>, or 
        /// <see cref="System.String.Empty">an empty string</see> if the <paramref name="value"/> was not found in the string.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="IndexOutOfRangeException">
        /// <paramref name="startIndex"/> specifies a position that is greater than the length of the string <paramref name="str"/>, 
        /// or is less than zero.
        /// </exception>
        public static string TakeAfterFirst(this string str, string value, int startIndex)
        {
            return TakeAfterFirst(str, value, startIndex, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Takes the part of a string that follows the first occurrence of a given <paramref name="value"/>.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The <see cref="char"/> value to cut the string by.
        /// </param>
        /// <returns>
        /// The part of the original string that follows the first occurrence of a given <paramref name="value"/>, or 
        /// <see cref="System.String.Empty">an empty string</see> if the <paramref name="value"/> was not found in the string.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        public static string TakeAfterFirst(this string str, char value)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            return CutFromIndex((x, y) => x.IndexOf(y, 0), str, value, false);
        }

        /// <summary>
        /// Takes the part of a string that follows the first occurrence of a given <paramref name="value"/>.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The <see cref="char"/> value to cut the string by.
        /// </param>
        /// <param name="startIndex">
        /// The index to start the searching from.
        /// </param>
        /// <returns>
        /// The part of the original string that follows the first occurrence of a given <paramref name="value"/>, or 
        /// <see cref="System.String.Empty">an empty string</see> if the <paramref name="value"/> was not found in the string.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="IndexOutOfRangeException">
        /// <paramref name="startIndex"/> specifies a position that is greater than the length of the string <paramref name="str"/>, 
        /// or is less than zero.
        /// </exception>
        public static string TakeAfterFirst(this string str, char value, int startIndex)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            return CutFromIndex((x, y) => x.IndexOf(x, y, startIndex), str, value, false);
        }
        #endregion TakeAfterFirst(...)

        #region TakeAfterLast(...)
        /// <summary>
        /// Takes the part of a string that follows the last occurrence of a given <paramref name="value"/>.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The <see cref="string"/> value to cut the string by.
        /// </param>
        /// <param name="startIndex">
        /// The index to start the searching from.
        /// </param>
        /// <param name="comparison">
        /// One of the <see cref="StringComparison"/> values, determining the string comparison method to be used for searching.
        /// </param>
        /// <returns>
        /// The part of the original string that follows the last occurrence of a given <paramref name="value"/>, or 
        /// <see cref="System.String.Empty">an empty string</see> if the <paramref name="value"/> was not found in the string.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="IndexOutOfRangeException">
        /// <paramref name="startIndex"/> specifies a position that is greater than the length of the string <paramref name="str"/>, 
        /// or is less than zero.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="comparison"/> is not a valid <see cref="StringComparison"/> value.
        /// </exception>
        public static string TakeAfterLast(this string str, string value, int startIndex, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            return CutFromIndex((x, y, z) => x.LastIndexOf(y, startIndex, z), str, value, comparison, false);
        }

        /// <summary>
        /// Takes the part of a string that follows the last occurrence of a given <paramref name="value"/>.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The <see cref="string"/> value to cut the string by.
        /// </param>
        /// <param name="comparison">
        /// One of the <see cref="StringComparison"/> values, determining the string comparison method to be used for searching.
        /// </param>
        /// <returns>
        /// The part of the original string that follows the last occurrence of a given <paramref name="value"/>, or 
        /// <see cref="System.String.Empty">an empty string</see> if the <paramref name="value"/> was not found in the string.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="comparison"/> is not a valid <see cref="StringComparison"/> value.
        /// </exception>
        public static string TakeAfterLast(this string str, string value, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            return CutFromIndex((x, y, z) => x.LastIndexOf(y, z), str, value, comparison, false);
        }

        /// <summary>
        /// Takes the part of a string that follows the last occurrence of a given <paramref name="value"/>.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The <see cref="string"/> value to cut the string by.
        /// </param>
        /// <returns>
        /// The part of the original string that follows the last occurrence of a given <paramref name="value"/>, or 
        /// <see cref="System.String.Empty">an empty string</see> if the <paramref name="value"/> was not found in the string.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        public static string TakeAfterLast(this string str, string value)
        {
            return TakeAfterLast(str, value, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Takes the part of a string that follows the last occurrence of a given <paramref name="value"/>.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The <see cref="string"/> value to cut the string by.
        /// </param>
        /// <param name="startIndex">
        /// The index to start the searching from.
        /// </param>
        /// <returns>
        /// The part of the original string that follows the last occurrence of a given <paramref name="value"/>, or 
        /// <see cref="System.String.Empty">an empty string</see> if the <paramref name="value"/> was not found in the string.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="IndexOutOfRangeException">
        /// <paramref name="startIndex"/> specifies a position that is greater than the length of the string <paramref name="str"/>, 
        /// or is less than zero.
        /// </exception>
        public static string TakeAfterLast(this string str, string value, int startIndex)
        {
            return TakeAfterLast(str, value, startIndex, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Takes the part of a string that follows the last occurrence of a given <paramref name="value"/>.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The <see cref="char"/> value to cut the string by.
        /// </param>
        /// <param name="startIndex">
        /// The index to start the searching from.
        /// </param>
        /// <returns>
        /// The part of the original string that follows the last occurrence of a given <paramref name="value"/>, or 
        /// <see cref="System.String.Empty">an empty string</see> if the <paramref name="value"/> was not found in the string.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="IndexOutOfRangeException">
        /// <paramref name="startIndex"/> specifies a position that is greater than the length of the string <paramref name="str"/>, 
        /// or is less than zero.
        /// </exception>
        public static string TakeAfterLast(this string str, char value, int startIndex)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            return CutFromIndex((x, y) => x.LastIndexOf(x, y, startIndex), str, value, false);
        }

        /// <summary>
        /// Takes the part of a string that follows the last occurrence of a given <paramref name="value"/>.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The <see cref="char"/> value to cut the string by.
        /// </param>
        /// <returns>
        /// The part of the original string that follows the last occurrence of a given <paramref name="value"/>, or 
        /// <see cref="System.String.Empty">an empty string</see> if the <paramref name="value"/> was not found in the string.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        public static string TakeAfterLast(this string str, char value)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            return CutFromIndex((x, y) => x.LastIndexOf(y), str, value, false);
        }
        #endregion TakeAfterLast(...)

        #region TrimStart(...)
        /// <summary>
        /// Removes the leading occurrence of a given string <paramref name="value"/> from a target <see cref="string"/> instance.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The string value to be cut.
        /// </param>
        /// <param name="comparison">
        /// One of the <see cref="StringComparison"/> values, determining the string comparison method to be used for searching.
        /// </param>
        /// <returns>
        /// The string without the passed <paramref name="value"/> at the end, if found; otherwise the original string.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="comparison"/> is not a valid <see cref="StringComparison"/> value.
        /// </exception>
        public static string TrimStart(this string str, string value, StringComparison comparison)
        {
            return str.VerifyArgument(nameof(str)).IsNotNull().Value.StartsWith(value, comparison)
                ? str.TakeAfterFirst(value, 0, comparison)
                : str;
        }

        /// <summary>
        /// Removes the leading occurrence of a given string <paramref name="value"/> from a target <see cref="string"/> instance.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The string value to be cut.
        /// </param>
        /// <returns>
        /// The string without the passed <paramref name="value"/> at the end, if found; otherwise the original string.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        public static string TrimStart(this string str, string value) { return TrimStart(str, value, StringComparison.CurrentCulture); }
        #endregion TrimStart(...)

        #region TrimEnd(...)
        /// <summary>
        /// Removes the trailing occurrence of a given string <paramref name="value"/> from a target <see cref="string"/> instance.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The string value to be cut.
        /// </param>
        /// <param name="comparison">
        /// One of the <see cref="StringComparison"/> values, determining the string comparison method to be used for searching.
        /// </param>
        /// <returns>
        /// The string without the passed <paramref name="value"/> at the end, if found; otherwise the original string.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="comparison"/> is not a valid <see cref="StringComparison"/> value.
        /// </exception>
        public static string TrimEnd(this string str, string value, StringComparison comparison)
        {
            return str.VerifyArgument(nameof(str)).IsNotNull().Value.EndsWith(value, comparison)
                ? str.TakeBeforeLast(value, comparison)
                : str;
        }

        /// <summary>
        /// Removes the trailing occurrence of a given string <paramref name="value"/> from a target <see cref="string"/> instance.
        /// </summary>
        /// <param name="str">
        /// The target <see cref="string"/> instance this extension method is called on. 
        /// </param>
        /// <param name="value">
        /// The string value to be cut.
        /// </param>
        /// <returns>
        /// The string without the passed <paramref name="value"/> at the end, if found; otherwise the original string.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        public static string TrimEnd(this string str, string value) { return TrimEnd(str, value, StringComparison.CurrentCulture); }
        #endregion TrimEnd(...)

        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        private static string JoinInternal(string[] values, string separator) { return string.Join(separator, values); }

        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
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