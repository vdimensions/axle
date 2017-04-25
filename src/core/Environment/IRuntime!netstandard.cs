using System;
using System.Reflection;
using System.Security.Policy;


namespace Axle.Environment
{
    /// <summary>
    /// An interface serving as an abstraction to a .NET runtime.
    /// </summary>
    public partial interface IRuntime
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
        /// Returns a reference to the <see cref="AppDomain"/> instance hosting the current .NET runtime. 
        /// </summary>
        AppDomain Domain { get; }
    }
}