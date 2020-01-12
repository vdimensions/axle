using System.IO;
using System.Text;

namespace Axle.Text.StructuredData
{
    public interface IStructuredDataReader
    {
        IStructuredDataRoot Read(Stream stream, Encoding encoding);
        IStructuredDataRoot Read(string data);
    }
}