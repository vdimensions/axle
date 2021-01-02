#if NETSTANDARD || NET20_OR_NEWER || UNITY_2018_1_OR_NEWER
using System.Collections.Generic;
#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
using System;
using System.Text;
using System.Globalization;
#endif


namespace Axle.Environment
{
    /// <summary>
    /// An interface representing an application's execution environment and its properties.
    /// </summary>
    public interface IEnvironment
    {
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
        /// <summary>
        /// Retrieves an environment variable value with a given <paramref name="name"/> for the scope of the current
        /// process.
        /// </summary>
        /// <param name="name">
        /// The name of the environment variable to retrieve. Names are <em>case-sensitive</em>.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> value for the environment variable with the provided <paramref name="name"/> for
        /// the scope of the current process; otherwise, <c>null</c>.
        /// </returns>
        string GetEnvironmentVariable(string name);
        #endif
        
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
        /// <summary>
        /// Retrieves an environment variable value with a given <paramref name="name"/> from the specified
        /// <paramref name="scope"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the environment variable to retrieve. Names are <em>case-sensitive</em>.
        /// </param>
        /// <param name="scope">
        /// One of the <see cref="EnvironmentVariableScope"/> values defining the level at which environment variables
        /// will be retrieved.
        /// </param>
        /// <remarks>
        /// The <paramref name="scope"/> parameter is optional, <see cref="EnvironmentVariableScope.Process"/> is used
        /// as a default value.
        /// </remarks>
        /// <returns>
        /// A <see cref="string"/> value for the environment variable with the provided <paramref name="name"/> and
        /// the source specified by the <paramref name="scope"/> parameter; otherwise, <c>null</c>.
        /// </returns>
        string GetEnvironmentVariable(string name, EnvironmentVariableScope scope);
        #endif
        
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
        /// <summary>
        /// Retrieves all environment variable names and their values for the scope of the current process.
        /// </summary>
        /// <returns>
        /// A dictionary that contains all environment variable names and their values for the scope of the current
        /// process; otherwise, an empty dictionary if no environment variables are found.
        /// </returns>
        IDictionary<string, string> GetEnvironmentVariables();
        #endif
        
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
        /// <summary>
        /// Retrieves all environment variable names and their values from the specified <paramref name="scope"/>.
        /// </summary>
        /// <param name="scope">
        /// One of the <see cref="EnvironmentVariableScope"/> values defining the level at which environment variables
        /// will be retrieved.
        /// </param>
        /// <remarks>
        /// The <paramref name="scope"/> parameter is optional, <see cref="EnvironmentVariableScope.Process"/> is used
        /// as a default value.
        /// </remarks>
        /// <returns>
        /// A dictionary that contains all environment variable names and their values from the source specified by the
        /// <paramref name="scope"/> parameter; otherwise, an empty dictionary if no environment variables are found.
        /// </returns>
        IDictionary<string, string> GetEnvironmentVariables(EnvironmentVariableScope scope);
        #endif

        /// <summary>
        /// Indicates the byte order ("endianness") in which data is stored in the platform's computer architecture.
        /// </summary>
        Endianness Endianness { get; }

        /// <summary>
        /// Gets the number of processors on the current machine.
        /// </summary>
        int ProcessorCount { get; }

        /// <summary>
        /// Gets the default line terminator character for the current platform.
        /// </summary>
        string NewLine { get; }

        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
        /// <summary>
        /// Gets the default path separator character for the current platform.
        /// </summary>
        char PathSeparator { get; }
        #endif

        #if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER

        /// <summary>
        /// Gets the NetBIOS name of the current platform.
        /// </summary>
        string MachineName { get; }
        #endif

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
        /// <summary>
        /// Gets the <see cref="CultureInfo"/> that represents the culture installed with the current operating system.
        /// </summary>
        CultureInfo Culture { get; }

        /// <summary>
        /// Gets an encoding for the platform operating system's ANSI code page.
        /// </summary>
        Encoding DefaultEncoding { get; }

        /// <summary>
        /// Gets an <see cref="OperatingSystem"/> object that contains the platform's OS identifier and version number.
        /// </summary>
        OperatingSystem OperatingSystem { get; }

        /// <summary>
        /// Gets the operating system identifier for the current platform.
        /// </summary>
        OperatingSystemID OperatingSystemID { get; }
        #endif
        
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
        /// <summary>
        /// Retrieves an environment variable value with a given <paramref name="name"/> for the scope of the current
        /// process.
        /// </summary>
        /// <param name="name">
        /// The name of the environment variable to retrieve. Names are <em>case-sensitive</em>.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> value for the environment variable with the provided <paramref name="name"/> for
        /// the scope of the current process; otherwise, <c>null</c>.
        /// </returns>
        string this[string name] { get; }
        #endif
    }
}
#endif