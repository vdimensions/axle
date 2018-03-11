#if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
using System;
using System.Globalization;

using Axle.Environment;


namespace Axle.Reflection.Extensions.Assembly
{
    using Assembly = System.Reflection.Assembly;

    public static class AssemblyExtensions
    {
        /// <summary>
        /// Loads a satellite assembly to the specified <paramref name="assembly">target</paramref> assembly and <paramref name="culture"/>.
        /// </summary>
        /// <param name="assembly">
        /// The target <see cref="Assembly"/> to search for related satellite assemblies.
        /// </param>
        /// <param name="culture">
        /// A <see cref="System.Globalization.CultureInfo"/> object specifying the culture for the requested satellite assembly.
        /// </param>
        /// <returns>
        /// A satellite assembly to the specified <paramref name="assembly">target</paramref> assembly and <paramref name="culture"/>.
        /// This method can return <c>null</c> in case a satellite assembly was not found for the given culture.
        /// In case the passed in <paramref name="culture"/> object represents the <see cref="System.Globalization.CultureInfo.InvariantCulture">invariant culture</see>
        /// then this method also returns <c>null</c>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either the <paramref name="assembly"/> or <paramref name="culture"/> parameter is <c>null</c>.
        /// </exception>
        /// <seealso cref="IRuntime.LoadSatelliteAssembly"/>
        public static Assembly LoadSatelliteAssembly(this Assembly assembly, CultureInfo culture)
        {
            return Platform.Runtime.LoadSatelliteAssembly(assembly, culture);
        }
    }
}
#endif