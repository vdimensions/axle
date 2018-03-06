using System.Text;


namespace Axle.Security.Cryptography.Algorithms.Sdk
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public abstract class AbstractCryptographicAlgorithm : AbstractEncryptionAlgorithm, ICryptographicAlgorithm
    {
        protected AbstractCryptographicAlgorithm() : base() { }

        public string Decrypt(string value, Encoding encoding) => DoDecrypt(value, encoding);
        public string Decrypt(byte[] value, Encoding encoding) => DoDecrypt(value, encoding);

        public byte[] Decrypt(byte[] value) => DoDecrypt(value);

        protected abstract string DoDecrypt(string value, Encoding encoding);

        protected virtual string DoDecrypt(byte[] value, Encoding encoding) => encoding.GetString(DoDecrypt(value));
        protected abstract byte[] DoDecrypt(byte[] value);
    }
}