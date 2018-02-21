using System;
using System.Security.Cryptography;
using System.Text;


namespace Axle.Security.Cryptography.Algorithms
{
    public interface IEncryptionAlgorithm : IDisposable
    {
        ICryptoTransform CreateEncryptor();
        ICryptoTransform CreateDecryptor();

        byte[] Encrypt(byte[] value);
        string Encrypt(string value, Encoding encoding);

        /// <summary>
        /// Converts a given binary data to a string representation.
        /// </summary>
        /// <param name="bytes">The data, as a byte array, which will be converted.</param>
        /// <returns>A string representation of adata given as <paramref name="bytes"/></returns>
        string ToString(byte[] bytes);
    }
}