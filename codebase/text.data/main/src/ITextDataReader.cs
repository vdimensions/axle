using System.IO;
using System.Text;

namespace Axle.Text.Data
{
    public interface ITextDataReader
    {
        ITextDataRoot Read(Stream stream, Encoding encoding);
        ITextDataRoot Read(string data);
    }
}