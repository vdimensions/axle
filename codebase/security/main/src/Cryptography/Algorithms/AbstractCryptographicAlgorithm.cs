using System.Text;

namespace Axle.Security.Cryptography.Algorithms
{
    public abstract class AbstractCryptographicAlgorithm : AbstractEncryptionAlgorithm, ICryptographicAlgorithm
    {
        protected AbstractCryptographicAlgorithm() : base() { }

        public string Decrypt(string value, Encoding encoding) => DoDecrypt(value, encoding);
        public string Decrypt(byte[] value, Encoding encoding) => DoDecrypt(value, encoding);

        public byte[] Decrypt(byte[] value) => DoDecrypt(value);

        protected abstract string DoDecrypt(string value, Encoding encoding);
        protected abstract byte[] DoDecrypt(byte[] value);

        protected virtual string DoDecrypt(byte[] value, Encoding encoding) => encoding.GetString(DoDecrypt(value));
    }
}