using System.Globalization;
using System.Text;


namespace Axle.Environment
{
    internal sealed partial class EnvironmentInfo : IEnvironment
    {
        public Endianness Endianness { get; private set; }
        public Encoding DefaultEncoding { get; private set; }
        public int ProcessorCount { get; private set; }
        public CultureInfo Culture { get; private set; }
        public string MachineName { get; private set; }
        public string NewLine { get; private set; }
        public char PathSeparator { get; private set; }
    }
}