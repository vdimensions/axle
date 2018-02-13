﻿using System;
using System.Collections.Generic;
using System.Reflection;


namespace Axle.Environment
{
	/// <summary>
	/// An interface serving as an abstraction to a .NET runtime.
	/// </summary>
    public partial interface IRuntime
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

        IEnumerable<Assembly> GetAssemblies();

        /// <summary>
        /// Instructs the current runtime to load the assembly specified by the 
        /// <paramref name="assemblyName"/> parameter. 
        /// </summary>
        /// <param name="assemblyName">
        /// The name of the assebly to be loaded. 
        /// </param>
        /// <returns>
        /// An <see cref="Assembly"/> object corresponding to the given <paramref name="assemblyName"/> parameter. 
        /// </returns>
        Assembly LoadAssembly(string assemblyName);

        #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
        /// <summary>
        /// Loads a satellite assembly to the specified <paramref name="targetAssembly">target</paramref> assembly and <paramref name="culture"/>.
        /// </summary>
        /// <param name="targetAssembly">
        /// The target <see cref="Assembly"/> to search for related satellite assemblies.
        /// </param>
        /// <param name="culture">
        /// A <see cref="System.Globalization.CultureInfo"/> object specifying the culture for the requested satellite assembly.
        /// </param>
        /// <returns>
        /// A satellite assembly to the specified <paramref name="targetAssembly">target</paramref> assembly and <paramref name="culture"/>.
        /// This method can return <c>null</c> in case a satellite assembly was not found for the given cutlure.
        /// In case the passed in <paramref name="culture"/> object represents the <see cref="System.Globalization.CultureInfo.InvariantCulture">invariant culture</see>
        /// then this method also returns <c>null</c>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either the <paramref name="targetAssembly"/> or <paramref name="culture"/> parameter is <c>null</c>.
        /// </exception>
        Assembly LoadSatelliteAssembly(Assembly targetAssembly, System.Globalization.CultureInfo culture);
        #endif

        /// <summary>
        /// Gets a <see cref="System.Version"/> object that describes the major, minor, build, and revision numbers of 
        /// the current CLR implementation.
        /// </summary>
        Version Version { get; }

        /// <summary>
        /// Gets a <see cref="System.Version"/> object that describes the major, minor, build, and revision numbers of 
        /// the common language runtime that is supported by the current CLR implementation.
        /// </summary>
        Version FrameworkVersion { get; }

        /// <summary>
        /// Gets a <see cref="RuntimeImplementation"/> value that describes the type of the .NET runtime that executes the current code.
        /// </summary>
        RuntimeImplementation Implementation { get; }

        #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
        /// <summary>
        /// Returns a reference to the <see cref="AppDomain"/> instance hosting the current .NET runtime. 
        /// </summary>
        AppDomain Domain { get; }
        #endif
    }
}