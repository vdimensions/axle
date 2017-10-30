using System;


namespace Axle.Environment
{
    public partial interface IEnvironment 
    {
        /// <summary>
        /// Gets an <see cref="OperatingSystem"/> object that contains the platform's OS identifier and version number.
        /// </summary>
        OperatingSystem OperatingSystem { get; }

        /// <summary>
        /// Gets the operating system identifier for the current platform. 
        /// </summary>
        OperatingSystemID OperatingSystemID { get; }

        /// <summary>
        /// Gets the timezone on the current platform.
        /// </summary>
        TimeZone TimeZone { get; }
    }
}