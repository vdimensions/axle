using System;
using System.Text;


namespace Axle.Security.Cryptography.Algorithms
{
    public interface ICryptographicAlgorithm : IEncryptionAlgorithm, IDisposable
    {
        string Decrypt(string value, Encoding encoding);
        string Decrypt(byte[] value, Encoding encoding);

        byte[] Decrypt(byte[] value);
    }
}