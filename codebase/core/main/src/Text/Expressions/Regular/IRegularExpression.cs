#if NETSTANDARD || NET20_OR_NEWER
using System;
using System.Text.RegularExpressions;


namespace Axle.Text.Expressions.Regular
{
    /// <summary>
    /// An interface representing a regular expression.
    /// </summary>
    public interface IRegularExpression
    {
        /// <summary>
        /// Indicates whether the current <see cref="IRegularExpression"/> finds a match in a specified <paramref name="input"/> string.
        /// </summary>
        /// <param name="input">
        /// The string to search for a match.
        /// </param>
        /// <returns>
        /// <c>true</c> if the regular expression finds a match; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="input"/> is <c>null</c>.
        /// </exception>
        bool IsMatch(string input);

        /// <summary>
        /// Indicates whether the current <see cref="IRegularExpression"/> finds a match in a specified input string.
        /// </summary>
        /// <param name="input">
        /// The string to search for a match.
        /// </param>
        /// <param name="startIndex">
        /// The character position at which to start the search.
        /// </param>
        /// <returns>
        /// <c>true</c> if the regular expression finds a match; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="input"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="startIndex"/> is less than zero or greater than the length of the <paramref name="input"/>.
        /// </exception>
        bool IsMatch(string input, int startIndex);


        /// <summary>
        /// Searches the specified <paramref name="input"/> string for all occurrences of a regular expression.
        /// </summary>
        /// <param name="input">
        /// The string to search for a match.
        /// </param>
        /// <returns>
        /// An array of the <see cref="System.Text.RegularExpressions.Match"/> objects found by the search.
        /// If no matches are found, the method returns an empty array.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="input"/> is <c>null</c>.
        /// </exception>
        Match[] Match(string input);

        /// <summary>
        /// Searches the specified <paramref name="input"/> string for all occurrences of a regular expression.
        /// </summary>
        /// <param name="input">
        /// The string to search for a match.
        /// </param>
        /// <param name="startIndex">
        /// The character position in the <paramref name="input"/> string at which to start the search.
        /// </param>
        /// <returns>
        /// An array of the <see cref="System.Text.RegularExpressions.Match"/> objects found by the search.
        /// If no matches are found, the method returns an empty array.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="input"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="startIndex"/> is less than zero or greater than the length of the <paramref name="input"/>.
        /// </exception>
        Match[] Match(string input, int startIndex);

        /// <summary>
        /// Splits an <paramref name="input"/> string into an array of substrings,
        /// at the positions defined by the current <see cref="IRegularExpression"/> instance.
        /// </summary>
        /// <param name="input">
        /// The string to be split.
        /// </param>
        /// <returns>
        /// An array of strings.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="input"/> is <c>null</c>.
        /// </exception>
        string[] Split(string input);

        /// <summary>
        /// Splits an <paramref name="input"/> string a specified maximum number of times into an array of substrings,
        /// at the positions defined by the current <see cref="IRegularExpression"/> instance.
        /// </summary>
        /// <param name="input">
        /// The string to be split.
        /// </param>
        /// <param name="count">
        /// The maximum number of times the split can occur.
        /// </param>
        /// <returns>
        /// An array of strings.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="input"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="count"/> is less than zero.
        /// </exception>
        string[] Split(string input, int count);

        /// <summary>
        /// Splits an <paramref name="input"/> string a specified maximum number of times into an array of substrings,
        /// at the positions defined by the current <see cref="IRegularExpression"/> instance.
        /// The search for the regular expression pattern starts at a specified character position in the <paramref name="input"/> string.
        /// </summary>
        /// <param name="input">
        /// The string to be split.
        /// </param>
        /// <param name="count">
        /// The maximum number of times the split can occur.
        /// </param>
        /// <param name="startIndex">
        /// The character position in the <paramref name="input"/> string where the search will begin.
        /// </param>
        /// <returns>
        /// An array of strings.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="input"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Either <paramref name="count"/> is less than zero, or <paramref name="startIndex"/> is less than zero or greater than the length of the <paramref name="input"/>.
        /// </exception>
        string[] Split(string input, int count, int startIndex);
    }
}
#endif