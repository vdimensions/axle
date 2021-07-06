#if NETSTANDARD || NET35_OR_NEWER
using System.Collections.Generic;

using Axle.Verification;


namespace Axle.Globalization.Extensions.CultureInfo
{
    using CultureInfo = System.Globalization.CultureInfo;

    /// <summary>
    /// A <see langword="static"/> class providing extension methods to 
    /// <see cref="CultureInfo"/> instances.
    /// </summary>
    public static class CultureInfoExtensions
    {
        /// <summary>
        /// Returns a collection of <see cref="CultureInfo" /> instances that include the current culture and all parent
        /// cultures. The cultures are ordered by their level of relation, starting from the culture specified by the
        /// <paramref name="culture"/> parameter and continuing with its parent culture. The resulting list should
        /// represent the natural culture fallback order that occurs during localization.
        /// </summary>
        /// <param name="culture">
        /// The <see cref="CultureInfo">culture</see> instance to expand.
        /// </param>
        /// <returns>
        /// A collection of <see cref="CultureInfo" /> instances that include the current culture and its parent
        /// cultures.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="culture"/> is <c>null</c>
        /// </exception>
        /// <example>
        /// For instance, if we take the culture <c>bg-BG</c> (The Bulgarian official culture), the method will return
        /// the following list of culture objects:
        /// <list type="bullet">
        ///   <item>
        ///     <term>bg-BG</term>  
        ///     <description>
        ///     <para>
        ///     Identical to the culture we have called the method on. Indeed, in terms of culture definition in .NET
        ///     this one has both country and language identifiers, making it the most-concrete of the ones listed.
        ///     </para>
        ///     </description>  
        ///   </item>
        ///   <item>
        ///     <term>bg</term>  
        ///     <description>
        ///     <para>
        ///     The parent of the above culture, including only the language identifier.
        ///     </para>
        ///     </description>  
        ///   </item>
        ///   <item>
        ///     <term><see cref="CultureInfo.InvariantCulture">invariant culture</see></term>
        ///     <description>
        ///     <para>
        ///     A reference to an <see cref="CultureInfo.InvariantCulture">invariant culture</see> object, which is the
        ///     ultimate parent for all culture instances.
        ///     </para>
        ///     </description>  
        ///   </item>
        /// </list>
        /// If we called the method on the culture <c>en</c> we will receive the following list: 
        /// <list type="bullet">
        ///   <item>
        ///     <term>en</term>
        ///     <description>
        ///     <para>
        ///     Again the instance we called the method on
        ///     </para>
        ///     </description>
        ///   </item>
        ///   <item>
        ///     <term><see cref="CultureInfo.InvariantCulture">invariant culture</see></term>
        ///     <description>
        ///     <para>
        ///     The only parent here is the <see cref="CultureInfo.InvariantCulture">invariant culture</see> object, as culture <c>en</c> has only the language qualifier.
        ///     </para>
        ///     </description>
        ///   </item>
        /// </list>
        /// </example>
        public static CultureInfo[] ExpandHierarchy(this CultureInfo culture)
        {
            var c = culture.VerifyArgument(nameof(culture)).IsNotNull().Value;
            var result = new List<CultureInfo>(3) { c };
            do
            {
                c = c.Parent;
                result.Add(c);
            }
            while (!ReferenceEquals(c, c.Parent) || !string.IsNullOrEmpty(c.Name));
            return result.ToArray();
        }
    }
}
#endif