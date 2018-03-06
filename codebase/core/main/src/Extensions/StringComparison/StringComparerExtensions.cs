using System;


namespace Axle.Extensions.StringComparison
{
    /// <summary>
    /// A static class containing common extension methods to <see cref="StringComparer"/> instances.
    /// </summary>
    public static class StringComparerExtensions
    {
        /// <summary>
        /// Gets the respective <see cref="StringComparer"/> implementation corresponding to 
        /// the given <paramref name="stringComparison"/> value.
        /// </summary>
        /// <param name="stringComparison">One of the <see cref="StringComparison"/> values</param>
        /// <returns>
        /// A <seealso cref="StringComparer"/> instance that corresponds to the given 
        /// <paramref name="stringComparison"/> value.
        /// </returns>
        public static StringComparer GetComparer(this System.StringComparison stringComparison)
        {
            switch (stringComparison)
            {
                case System.StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture;
                case System.StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase;
                #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
                case System.StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture;
                case System.StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase;
                #endif
                case System.StringComparison.Ordinal:
                    return StringComparer.Ordinal;
                case System.StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase;
                default:
                    return StringComparer.CurrentCulture;
            }
        }
    }
}