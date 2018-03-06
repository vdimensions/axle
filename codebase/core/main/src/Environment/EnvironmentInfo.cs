using System;
#if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
using System.Globalization;
using System.Text;
#endif


namespace Axle.Environment
{
    internal sealed class EnvironmentInfo : IEnvironment
    {
        #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
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

        #if NETSTANDARD1_5_OR_NEWER || !NETSTANDARD
        private EnvironmentInfo()
        #else
        internal EnvironmentInfo()
        #endif
        {
            Endianness = BitConverter.IsLittleEndian ? Endianness.LittleEndian : Endianness.BigEndian;
            ProcessorCount = System.Environment.ProcessorCount;
            NewLine = System.Environment.NewLine;
            #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
            Culture = CultureInfo.InstalledUICulture;
            DefaultEncoding = Encoding.Default;
            MachineName = System.Environment.MachineName;
            OperatingSystem = System.Environment.OSVersion;
            PathSeparator = System.IO.Path.PathSeparator;
            #endif
        }

        public Endianness Endianness { get; }
        public int ProcessorCount { get; }
        public string NewLine { get; }

        #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
        public CultureInfo Culture { get; }
        public Encoding DefaultEncoding { get; }
        public char PathSeparator { get; }
        public string MachineName { get; }
        public OperatingSystem OperatingSystem { get; }
        public OperatingSystemID OperatingSystemID => GetOSID(OperatingSystem);
        #endif
    }
}