using System;
using System.Globalization;
using System.Text;

using Axle.IO;


namespace Axle.Environment
{
    [Serializable]
    internal sealed partial class EnvironmentInfo : IEnvironment
    {
        internal static OperatingSystemID GetOSID(OperatingSystem os)
        {
            switch (os.Platform)
            {
                case PlatformID.Unix:
                    return OperatingSystemID.Unix;
                case PlatformID.MacOSX:
                    return OperatingSystemID.MacOS;
                case PlatformID.WinCE:
                    return OperatingSystemID.WinCE;
                case PlatformID.Win32S:
                    return OperatingSystemID.Win32S;
                case PlatformID.Win32NT:
                    return OperatingSystemID.Win32NT;
                case PlatformID.Win32Windows:
                    return OperatingSystemID.Win32Windows;
                case PlatformID.Xbox:
                    return OperatingSystemID.XBox;
                default:
                    return OperatingSystemID.Unknown;
            }
        }

        private EnvironmentInfo()
        {
            this.OperatingSystem = System.Environment.OSVersion;
            this.Endianness = BitConverter.IsLittleEndian ? Endianness.LittleEndian : Endianness.BigEndian;
            this.ProcessorCount = System.Environment.ProcessorCount;
            this.Culture = CultureInfo.InstalledUICulture;
            this.DefaultEncoding = Encoding.Default;
            this.TimeZone = TimeZone.CurrentTimeZone;
            this.MachineName = System.Environment.MachineName;
            this.NewLine = System.Environment.NewLine;
            this.PathSeparator = System.IO.Path.PathSeparator;
        }

        public OperatingSystem OperatingSystem { get; private set; }
        public OperatingSystemID OperatingSystemID { get { return GetOSID(OperatingSystem); } }
        public TimeZone TimeZone { get; private set; }
    }
}