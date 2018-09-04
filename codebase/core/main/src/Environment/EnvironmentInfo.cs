using System;
#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Globalization;
using System.Text;
#endif


namespace Axle.Environment
{
    internal sealed class EnvironmentInfo : IEnvironment
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        #if NETSTANDARD
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

        #if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
        private EnvironmentInfo()
        #else
        internal EnvironmentInfo()
        #endif
        {
            Endianness = BitConverter.IsLittleEndian ? Endianness.LittleEndian : Endianness.BigEndian;
            ProcessorCount = System.Environment.ProcessorCount;
            NewLine = System.Environment.NewLine;
            #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
            PathSeparator = System.IO.Path.PathSeparator;
            #endif
            #if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
            MachineName = System.Environment.MachineName;
            #endif
            #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
            Culture = CultureInfo.InstalledUICulture;
            DefaultEncoding = Encoding.Default;
            OperatingSystem = System.Environment.OSVersion;
            #endif
        }

        public Endianness Endianness { get; }
        public int ProcessorCount { get; }
        public string NewLine { get; }

        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        public char PathSeparator { get; }
        #endif
        #if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
        public string MachineName { get; }
        #endif
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        public CultureInfo Culture { get; }
        public Encoding DefaultEncoding { get; }
        public OperatingSystem OperatingSystem { get; }
        public OperatingSystemID OperatingSystemID => GetOSID(OperatingSystem);
        #endif
    }
}