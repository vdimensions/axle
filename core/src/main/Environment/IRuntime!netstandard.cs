using System;
using System.Globalization;
using System.Reflection;
using System.Security.Policy;


namespace Axle.Environment
{
    partial interface IRuntime
    {
        /// <summary>
        /// Instructs the current runtime to load the assembly specified by the 
        /// <paramref name="assemblyName"/>and <paramref name="securityEvidence"/> parameters. 
        /// </summary>
        /// <param name="assemblyName">
        /// The name of the assebly to be loaded. 
        /// </param>
        /// <param name="securityEvidence">
        /// The <see cref="Evidence"/> object to use when loading the assembly.  
        /// </param>
        /// <returns>
        /// An <see cref="Assembly"/> object corresponding to the given <paramref name="assemblyName"/> parameter. 
        /// </returns>
        Assembly LoadAssembly(string assemblyName, Evidence securityEvidence);

        /// <summary>
        /// Loads a satellite assembly to the specified <paramref name="targetAssembly">target</paramref> assembly and <paramref name="culture"/>.
        /// </summary>
        /// <param name="targetAssembly">
        /// The target <see cref="Assembly"/> to search for related satellite assemblies.
        /// </param>
        /// <param name="culture">
        /// A <see cref="CultureInfo"/> object specifying the culture for the requested satellite assembly.
        /// </param>
        /// <returns>
        /// A satellite assembly to the specified <paramref name="targetAssembly">target</paramref> assembly and <paramref name="culture"/>.
        /// This method can return <c>null</c> in case a satellite assembly was not found for the given cutlure.
        /// In case the passed in <paramref name="culture"/> object represents the <see cref="CultureInfo.InvariantCulture">invariant culture</see>
        /// then this method also returns <c>null</c>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either the <paramref name="targetAssembly"/> or <paramref name="culture"/> parameter is <c>null</c>.
        /// </exception>
        Assembly LoadSatelliteAssembly(Assembly targetAssembly, CultureInfo culture);

        /// <summary>
        /// Returns a reference to the <see cref="AppDomain"/> instance hosting the current .NET runtime. 
        /// </summary>
        AppDomain Domain { get; }
    }
}