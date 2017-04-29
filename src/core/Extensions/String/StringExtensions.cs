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
        public static bool Contains(this string str, string value, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }
            return str.IndexOf(value, comparison) >= 0;
        }
        public static bool Contains(this string str, string value)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }
            return str.IndexOf(value) >= 0;
        }
        public static bool Contains(this string str, string value, int startIndex, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }
            return str.IndexOf(value, startIndex, comparison) >= 0;
        }
        public static bool Contains(this string str, string value, int startIndex)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }
            return str.IndexOf(value, startIndex) >= 0;
        }
        public static bool Contains(this string str, string value, int startIndex, int count, StringComparison comparison)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }
            return str.IndexOf(value, startIndex, count, comparison) >= 0;
        }
        public static bool Contains(this string str, string value, int startIndex, int count)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }
            return str.IndexOf(value, startIndex, count) >= 0;
        }
        #endregion

        public static bool IsEmpty(this string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            return str.Length == 0;
        }

        public static bool IsNullOrEmpty(this string @this) { return string.IsNullOrEmpty(@this); }

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

        private static string JoinInternal(string[] values, string separator) { return string.Join(separator, values); }
        private static string JoinInternal(IEnumerable<string> values, string separator)
        {
            return string.Join(separator, values.ToArray());
        }

        public static string Join(this string separator, IEnumerable<string> values) { return JoinInternal(values, separator); }
        public static string Join(this char separator, IEnumerable<string> values) { return JoinInternal(values, separator.ToString()); }
        public static string Join(this string separator, params string[] values) { return JoinInternal(values, separator); }
        public static string Join(this char separator, params string[] values) { return JoinInternal(values, separator.ToString()); }
    }
}