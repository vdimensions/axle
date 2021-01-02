#if NETSTANDARD || NET20_OR_NEWER || UNITY_2018_1_OR_NEWER
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Axle.Verification;

namespace Axle.Extensions.String
{
    using StringComparison = System.StringComparison;

    /// <summary>
    /// A <see langword="static"/> class containing common extension methods to 
    /// <see cref="String"/> instances.
    /// </summary>
    public static class StringExtensions
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
        /// within the target <see cref="string"/> represented by the <paramref name="str"/> parameter; <c>false</c>
        /// otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="comparison"/> is not a valid <see cref="StringComparison"/> value.
        /// </exception>
        /// <seealso cref="StringComparison"/>
        /// <seealso cref="string.IndexOf(string, StringComparison)"/>
        public static bool Contains(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, string value, StringComparison comparison)
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
        /// within the target <see cref="string"/> represented by the <paramref name="str"/> parameter; <c>false</c>
        /// otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="str"/> or <paramref name="value"/> is <c>null</c>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <seealso cref="string.IndexOf(string)"/>
        [SuppressMessage("ReSharper", "StringIndexOfIsCultureSpecific.1")]
        public static bool Contains(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, string value)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
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
        /// within the target <see cref="string"/> represented by the <paramref name="str"/> parameter; <c>false</c>
        /// otherwise.
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
        public static bool Contains(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, string value, int startIndex, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
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
        [SuppressMessage("ReSharper", "StringIndexOfIsCultureSpecific.2")]
        public static bool Contains(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, string value, int startIndex)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
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
        /// <paramref name="count"/> is greater than the length of <paramref name="str"/> minus
        /// <paramref name="startIndex"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="comparison"/> is not a valid <see cref="StringComparison"/> value.
        /// </exception>
        /// <seealso cref="StringComparison"/>
        /// <seealso cref="string.IndexOf(string, int, int, StringComparison)"/>
        public static bool Contains(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, string value, int startIndex, int count, StringComparison comparison)
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
        /// <paramref name="count"/> is greater than the length of <paramref name="str"/> minus
        /// <paramref name="startIndex"/>.
        /// </exception>
        /// <seealso cref="string.IndexOf(string, int, int)"/>
        [SuppressMessage("ReSharper", "StringIndexOfIsCultureSpecific.3")]
        public static bool Contains(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, string value, int startIndex, int count)
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
        /// <c>true</c> if the <paramref name="str">target string</paramref> is an empty string instance; false
        /// otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="str"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="string.Empty"/>
        public static bool IsEmpty(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str)
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
        /// <c>true</c> if the <paramref name="str">target string</paramref> is <c>null</c> or an empty string instance;
        /// false otherwise.
        /// </returns>
        /// <seealso cref="string.Empty"/>
        /// <seealso cref="string.IsNullOrEmpty(string)"/>
        /// <seealso cref="IsEmpty(string)"/>
        public static bool IsNullOrEmpty(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str) => string.IsNullOrEmpty(str);

        /// <summary>
        /// Creates a <see cref="string"/> using all the characters from a target string instance, but in a reversed
        /// order.
        /// </summary>
        /// <param name="str">The <see cref="string">string instance</see> upon which the extension method is called
        /// upon.</param>
        /// <returns>
        /// A new string instance using all the characters from a target string instance, but in a reversed order.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="str"/> is <c>null</c>.
        /// </exception>
        public static string Reverse(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str)
        {
            var chars = Verifier.IsNotNull(Verifier.VerifyArgument(str, nameof(str))).Value.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
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
        public static string[] Split(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, StringSplitOptions options, char separator)
        {
            return Verifier.IsNotNull(Verifier.VerifyArgument(str, nameof(str))).Value.Split(new[] { separator }, options);
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
        public static string[] Split(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, StringSplitOptions options, params char[] separators)
        {
            return Verifier.IsNotNull(Verifier.VerifyArgument(str, nameof(str))).Value.Split(separators, options);
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
        public static string[] Split(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, StringSplitOptions options, int count, params char[] separators)
        {
            return Verifier.IsNotNull(Verifier.VerifyArgument(str, nameof(str))).Value.Split(separators, count, options);
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
        public static string[] Split(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, StringSplitOptions options, params string[] separators)
        {
            return Verifier.IsNotNull(Verifier.VerifyArgument(str, nameof(str))).Value.Split(separators, options);
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
        public static string[] Split(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, StringSplitOptions options, int count, params string[] separators)
        {
            return Verifier.IsNotNull(Verifier.VerifyArgument(str, nameof(str))).Value.Split(separators, count, options);
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
        public static string[] Split(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, params string[] separators)
        {
            return Verifier.IsNotNull(Verifier.VerifyArgument(str, nameof(str))).Value.Split(separators, StringSplitOptions.None);
        }
        #endregion Split(...)

        #if NETSTANDARD || NET45_OR_NEWER || UNITY_2018_1_OR_NEWER
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
        #if NETSTANDARD || NET45_OR_NEWER || UNITY_2018_1_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        private static string CutFromIndex(
            Func<string, char, int> searchFunc, string stringToTrim, char charToSearch, bool trimEnd)
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
        public static string TakeBeforeFirst(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, string value, int startIndex, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (value.Length == 0)
            {
                return str;
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
        public static string TakeBeforeFirst(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, string value, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (value.Length == 0)
            {
                return str;
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
        public static string TakeBeforeFirst(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, string value, int startIndex)
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
        public static string TakeBeforeFirst(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, string value)
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
        public static string TakeBeforeFirst(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, char value, int startIndex)
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
        public static string TakeBeforeFirst(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, char value)
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
        public static string TakeBeforeLast(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, string value, int startIndex, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (value.Length == 0)
            {
                return str;
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
        public static string TakeBeforeLast(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, string value, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (value.Length == 0)
            {
                return str;
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
        public static string TakeBeforeLast(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, string value, int startIndex)
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
        public static string TakeBeforeLast(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, string value)
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
        public static string TakeBeforeLast(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, char value, int startIndex)
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
        public static string TakeBeforeLast(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, char value)
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
        public static string TakeAfterFirst(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, string value, int startIndex, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (value.Length == 0)
            {
                return str;
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
        public static string TakeAfterFirst(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, string value, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (value.Length == 0)
            {
                return str;
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
        public static string TakeAfterFirst(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, string value)
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
        public static string TakeAfterFirst(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, string value, int startIndex)
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
        public static string TakeAfterFirst(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, char value)
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
        public static string TakeAfterFirst(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, char value, int startIndex)
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
        public static string TakeAfterLast(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, string value, int startIndex, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (value.Length == 0)
            {
                return str;
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
        public static string TakeAfterLast(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, string value, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (value.Length == 0)
            {
                return str;
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
        public static string TakeAfterLast(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, string value)
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
        public static string TakeAfterLast(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, string value, int startIndex)
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
        public static string TakeAfterLast(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, char value, int startIndex)
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
        public static string TakeAfterLast(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, char value)
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
        public static string TrimStart(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, string value, StringComparison comparison)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(str, nameof(str)));
            return str.StartsWith(value, comparison)
                ? TakeAfterFirst(str, value, 0, comparison)
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
        public static string TrimStart(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, string value) => TrimStart(str, value, StringComparison.CurrentCulture);
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
        public static string TrimEnd(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, string value, StringComparison comparison)
        {
            return Verifier.IsNotNull(Verifier.VerifyArgument(str, nameof(str))).Value.EndsWith(value, comparison)
                ? TakeBeforeLast(str, value, comparison)
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
        public static string TrimEnd(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str, string value) { return TrimEnd(str, value, StringComparison.CurrentCulture); }
        #endregion TrimEnd(...)

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
        /// <summary>
        /// Retrieves the system's reference for the specified string.
        /// </summary>
        /// <param name="str">A <see cref="string"/> to search for in the intern pool.</param>
        /// <returns>
        /// The system's reference to <paramref name="str"/>, if it is interned; otherwise, a new reference to a
        /// <see cref="string"/> with the value of <paramref name="str"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="str"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="string.Intern(string)"/>
        public static string Intern(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string str) => string.Intern(Verifier.IsNotNull(Verifier.VerifyArgument(str, nameof(str))));
        #endif

        #if NETSTANDARD || NET45_OR_NEWER || UNITY_2018_1_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        private static string JoinInternal(string[] values, string separator) { return string.Join(separator, values); }

        #if NETSTANDARD || NET45_OR_NEWER || UNITY_2018_1_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        private static string JoinInternal(IEnumerable<string> values, string separator)
        {
            var strArr = new List<string>(values).ToArray();
            return string.Join(separator, strArr);
        }

        /// <summary>
        /// Concatenates all the elements of a string collection, using the specified <paramref name="separator"/>
        /// between each element.
        /// </summary>
        /// <param name="separator">
        /// The <see cref="string"/> to use as a separator. The <paramref name="separator"/> is included in the returned
        /// string only if <paramref name="values"/> has more than one element.
        /// </param>
        /// <param name="values">
        /// A collection that contains the elements to concatenate.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> that consists of the <paramref name="values"/> delimited by the
        /// <paramref name="separator"/> string.
        /// If the <paramref name="values"/> collection is empty, the method returns an
        /// <see cref="string.Empty">empty</see> string.
        /// </returns>
        public static string Join(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string separator, IEnumerable<string> values) => JoinInternal(values, separator);
        /// <summary>
        /// Concatenates all the elements of a string collection, using the specified <paramref name="separator"/>
        /// between each element.
        /// </summary>
        /// <param name="separator">
        /// The <see cref="char">character</see> to use as a separator. The <paramref name="separator"/> is included in
        /// the returned string only if <paramref name="values"/> has more than one element.
        /// </param>
        /// <param name="values">
        /// A collection that contains the elements to concatenate.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> that consists of the <paramref name="values"/> delimited by the
        /// <paramref name="separator"/> character.
        /// If the <paramref name="values"/> collection is empty, the method returns an
        /// <see cref="string.Empty">empty</see> string.
        /// </returns>
        public static string Join(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            char separator, IEnumerable<string> values) => JoinInternal(values, separator.ToString());
        /// <summary>
        /// Concatenates all the elements of a string array, using the specified <paramref name="separator"/> between
        /// each element.
        /// </summary>
        /// <param name="separator">
        /// The <see cref="string"/> to use as a separator. The <paramref name="separator"/> is included in the returned
        /// string only if <paramref name="values"/> has more than one element.
        /// </param>
        /// <param name="values">
        /// An array that contains the elements to concatenate.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> that consists of the <paramref name="values"/> delimited by the
        /// <paramref name="separator"/> string.
        /// If the <paramref name="values"/> array is empty, the method returns an
        /// <see cref="string.Empty">empty</see> string.
        /// </returns>
        public static string Join(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            string separator, params string[] values) => JoinInternal(values, separator);
        /// <summary>
        /// Concatenates all the elements of a string array, using the specified <paramref name="separator"/> between
        /// each element.
        /// </summary>
        /// <param name="separator">
        /// The <see cref="char">character</see> to use as a separator. The <paramref name="separator"/> is included in
        /// the returned string only if <paramref name="values"/> has more than one element.
        /// </param>
        /// <param name="values">
        /// An array that contains the elements to concatenate.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> that consists of the <paramref name="values"/> delimited by the
        /// <paramref name="separator"/> character.
        /// If the <paramref name="values"/> array is empty, the method returns an
        /// <see cref="string.Empty">empty</see> string.
        /// </returns>
        public static string Join(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            char separator, params string[] values) => JoinInternal(values, separator.ToString());
    }
}
#endif