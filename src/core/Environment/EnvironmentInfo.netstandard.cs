using System;
using System.Globalization;
using System.Text;


namespace Axle.Environment
{
    partial class EnvironmentInfo
    {
        private EnvironmentInfo()
        {
            this.Endianness = BitConverter.IsLittleEndian ? Endianness.LittleEndian : Endianness.BigEndian;
            this.ProcessorCount = System.Environment.ProcessorCount;
            //this.Culture = CultureInfo.InstalledUICulture;
            //this.DefaultEncoding = Encoding.Default;
            //this.MachineName = System.Environment.MachineName;
            this.NewLine = System.Environment.NewLine;
            //this.PathSeparator = System.IO.Path.PathSeparator;
        }
    }
}