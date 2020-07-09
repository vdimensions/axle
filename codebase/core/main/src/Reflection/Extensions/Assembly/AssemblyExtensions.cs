#if NETSTANDARD || NET35_OR_NEWER
#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Axle.Environment;
#if NET35_OR_NEWER && !NET45_OR_NEWER && !NETSTANDARD
using System.Linq;
using Axle.Verification;
#endif

namespace Axle.Reflection.Extensions.Assembly
{
    using Assembly = System.Reflection.Assembly;

    /// <summary>
    /// A class containing extension methods tied to the <see cref="Assembly"/> type.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Loads a satellite assembly to the specified <paramref name="assembly">target</paramref> assembly and
        /// <paramref name="culture"/>.
        /// </summary>
        /// <param name="assembly">
        /// The target <see cref="Assembly"/> to search for related satellite assemblies.
        /// </param>
        /// <param name="culture">
        /// A <see cref="System.Globalization.CultureInfo"/> object specifying the culture for the requested satellite
        /// assembly.
        /// </param>
        /// <returns>
        /// A satellite assembly to the specified <paramref name="assembly">target</paramref> assembly and
        /// <paramref name="culture"/>. This method can return <c>null</c> in case a satellite assembly was not found
        /// for the given culture. In case the passed in <paramref name="culture"/> object represents the
        /// <see cref="System.Globalization.CultureInfo.InvariantCulture">invariant culture</see> then this method also
        /// returns <c>null</c>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either the <paramref name="assembly"/> or <paramref name="culture"/> parameter is <c>null</c>.
        /// </exception>
        /// <seealso cref="IRuntime.LoadSatelliteAssembly"/>
        public static Assembly LoadSatelliteAssembly(this Assembly assembly, CultureInfo culture)
        {
            return Platform.Runtime.LoadSatelliteAssembly(assembly, culture);
        }

        #if NET35_OR_NEWER && !NET45_OR_NEWER && !NETSTANDARD
        /// <summary>
        /// Gets the custom attributes for this assembly as specified by <typeparamref name="TAttribute" />.
        /// </summary>
        /// <param name="assembly">
        /// The <see cref="Assembly"/> to be searched for attributes.
        /// </param>
        /// <typeparam name="TAttribute">
        /// The <see cref="Type"/> for which the custom attributes are to be returned.
        /// </typeparam>
        /// <returns>
        /// An array of type <typeparamref name="TAttribute" /> containing the custom attributes for this assembly of
        /// that type.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="assembly"/> is <c>null</c>.
        /// </exception>
        public static TAttribute[] GetCustomAttributes<TAttribute>(this Assembly assembly) where TAttribute : Attribute
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(assembly, nameof(assembly)));
            return Enumerable.ToArray(
                Enumerable.Cast<TAttribute>(
                    assembly.GetCustomAttributes(typeof(TAttribute), true)));
        }
        #endif
    }
}
#endif
#endif