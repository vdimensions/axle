using System;
using System.Globalization;
using System.Text;


namespace Axle.Environment
{
    internal sealed partial class EnvironmentInfo : IEnvironment
    {
        #if !NETSTANDARD

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

        public TimeZone TimeZone { get; private set; }

        #elif NETSTANDARD1_5_OR_NEWER

        private EnvironmentInfo()
        {
            #if NETSTANDARD2_0_OR_NEWER
            this.OperatingSystem = System.Environment.OSVersion;
            #endif
            this.Endianness = BitConverter.IsLittleEndian ? Endianness.LittleEndian : Endianness.BigEndian;
            this.ProcessorCount = System.Environment.ProcessorCount;
            //this.Culture = CultureInfo.InstalledUICulture;
            //this.DefaultEncoding = Encoding.Default;
            //this.MachineName = System.Environment.MachineName;
            this.NewLine = System.Environment.NewLine;
            //this.PathSeparator = System.IO.Path.PathSeparator;
        }

        #endif

        #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
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

        public OperatingSystem OperatingSystem { get; private set; }
        public OperatingSystemID OperatingSystemID => GetOSID(OperatingSystem);
        #endif

        public Endianness Endianness { get; private set; }
        public Encoding DefaultEncoding { get; private set; }
        public int ProcessorCount { get; private set; }
        public CultureInfo Culture { get; private set; }
        public string MachineName { get; private set; }
        public string NewLine { get; private set; }
        public char PathSeparator { get; private set; }
    }
}