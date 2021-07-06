using System;
using System.Security.Cryptography;
using System.Text;

namespace Axle.Security.Cryptography.Algorithms
{
    public sealed class Base64EncodeAlgorithm : AbstractCryptographicAlgorithm
    {
        public Base64EncodeAlgorithm() : base() { }

        public override ICryptoTransform CreateEncryptor() => new ToBase64Transform();

        public override ICryptoTransform CreateDecryptor() => new FromBase64Transform();

        public override byte[] Encrypt(byte[] value)
        {
            var encoding = Encoding.ASCII;
            return encoding.GetBytes(Encrypt(encoding.GetString(value), encoding));
        }
        public override string Encrypt(string value, Encoding encoding)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }
            if (value.Length == 0)
            {
                return string.Empty;
            }
            return Convert.ToBase64String(encoding.GetBytes(value));
        }

        public byte[] Decrypt(string value) => Convert.FromBase64String(value);

        protected override string DoDecrypt(string value, Encoding encoding)
        {
            var trueBytes = Decrypt(value);
            var decoder = encoding.GetDecoder();
            var charCount = decoder.GetCharCount(trueBytes, 0, trueBytes.Length);
            var characters = new char[charCount];
            decoder.GetChars(trueBytes, 0, trueBytes.Length, characters, 0);
            return new string(characters);
        }
        protected override string DoDecrypt(byte[] value, Encoding encoding) => DoDecrypt(encoding.GetString(value), encoding);

        protected override byte[] DoDecrypt(byte[] value)
        {
            var str = Encoding.ASCII.GetString(value);
            return Decrypt(str);
        }

        protected override void Dispose(bool disposing) { }

        public override string ToString(byte[] bytes) => Convert.ToBase64String(bytes);
    }
}