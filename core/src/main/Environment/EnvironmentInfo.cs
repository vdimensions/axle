using System;
#if !NETSTANDARD
using System.Globalization;
#endif
#if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
using System.Text;
#endif


namespace Axle.Environment
{
    internal sealed class EnvironmentInfo : IEnvironment
    {
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
        #endif

        #if !NETSTANDARD || NETSTANDARD1_5_OR_NEWER
        private EnvironmentInfo()
        #else
        internal EnvironmentInfo()
        #endif
        {
            this.Endianness = BitConverter.IsLittleEndian ? Endianness.LittleEndian : Endianness.BigEndian;
            this.ProcessorCount = System.Environment.ProcessorCount;
            this.NewLine = System.Environment.NewLine;
            #if !NETSTANDARD
            this.Culture = CultureInfo.InstalledUICulture;
            #endif
            #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
            this.DefaultEncoding = Encoding.Default;
            this.MachineName = System.Environment.MachineName;
            this.OperatingSystem = System.Environment.OSVersion;
            this.PathSeparator = System.IO.Path.PathSeparator;
            #endif
        }

        public Endianness Endianness { get; }
        public int ProcessorCount { get; }
        public string NewLine { get; }

        #if !NETSTANDARD
        public CultureInfo Culture { get; }
        #endif

        #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
        public Encoding DefaultEncoding { get; }
        public char PathSeparator { get; }
        public string MachineName { get; }
        public OperatingSystem OperatingSystem { get; }
        public OperatingSystemID OperatingSystemID => GetOSID(OperatingSystem);
        #endif
    }
}