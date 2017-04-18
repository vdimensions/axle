using System;
using System.Reflection;
using System.Security.Policy;


namespace Axle.Environment
{
	/// <summary>
	/// An interface serving as an abstraction to a .NET runtime.
	/// </summary>
    public interface IRuntime
    {
		/// <summary>
		/// Converts the provided by the <paramref name="resourceName"/> parameter string to
		/// a valid path for an embedded resource.
		/// </summary>
		/// <param name="resourceName">
		/// The resource name to be converted to an embedment path.
		/// </param>
		/// <returns>
		/// A valid embedded resource path.
		/// </returns>
        string GetEmbeddedResourcePath(string resourceName);

        Assembly LoadAssembly(string assemblyName);
        Assembly LoadAssembly(string assemblyName, Evidence securityEvidence);

        /// <summary>
        /// Returns a reference to the <see cref="AppDomain"/> instance hosting the current .NET runtime. 
        /// </summary>
        AppDomain Domain { get; }

        /// <summary>
        /// Gets a <see cref="System.Version"/> object that describes the major, minor, build, and revision numbers of the common language runtime.
        /// </summary>
        Version Version { get; }
    }
}