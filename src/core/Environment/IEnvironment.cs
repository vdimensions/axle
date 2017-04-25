using System;
using System.Globalization;
using System.Text;


namespace Axle.Environment
{
    /// <summary>
    /// An interface representing an application's execution environment and its properties.
    /// </summary>
    public interface IEnvironment 
    {
        /// <summary>
        /// Indicates the byte order ("endianness") in which data is stored in the platform's computer architecture.
        /// </summary>
        Endianness Endianness { get; }

        /// <summary>
        /// Gets an encoding for the platform operating system's ANSI code page.
        /// </summary>
        Encoding DefaultEncoding { get; }

#if !netstandard
        /// <summary>
        /// Gets an <see cref="OperatingSystem"/> object that contains the platform's OS identifier and version number.
        /// </summary>
        OperatingSystem OperatingSystem { get; }
#endif

        /// <summary>
        /// Gets the operating system identifier for the current platform. 
        /// </summary>
        OperatingSystemID OperatingSystemID { get; }

        /// <summary>
        /// Gets the number of processors on the current machine.
        /// </summary>
        int ProcessorCount { get; }

        /// <summary>
        /// Gets the <see cref="CultureInfo"/> that represents the culture installed with the current operating system.
        /// </summary>
        CultureInfo Culture { get; }

#if !netstandard
        /// <summary>
        /// Gets the timezone on the current platform.
        /// </summary>
        TimeZone TimeZone { get; }
#endif

        /// <summary>
        /// Gets the NetBIOS name of the current platform.
        /// </summary>
        string MachineName { get; }

        /// <summary>
        /// Gets the default line terminator character for the current platform. 
        /// </summary>
        string NewLine { get; }

        /// <summary>
        /// Gets the default path separator character for the current platform. 
        /// </summary>
        char PathSeparator { get; }
    }
}