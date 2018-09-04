using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

using Axle.Security.Cryptography.Algorithms.Sdk;


namespace Axle.Security.Cryptography.Algorithms.Symmetric.Sdk
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public abstract class AbstractSymmetricHashAlgorithm : AbstractCryptographicAlgorithm, ISymmetricHashAlgorithm
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly SymmetricAlgorithm hash;

        public override ICryptoTransform CreateEncryptor() => hash.CreateEncryptor();
        public override ICryptoTransform CreateDecryptor() => hash.CreateDecryptor();

        public int BlockSize
        {
            get => hash.BlockSize;
            set => hash.BlockSize = value;
        }

        public byte[] IV
        {
            get => hash.IV;
            set => hash.IV = value;
        }

        public byte[] Key
        {
            get => hash.Key;
            set => hash.Key = value;
        }

        public int KeySize
        {
            get => hash.KeySize;
            set => hash.KeySize = value;
        }

        public KeySizes[] LegalBlockKeySizes => hash.LegalBlockSizes;

        public KeySizes[] LegalKeySizes => hash.LegalKeySizes;

        public CipherMode Mode
        {
            get => hash.Mode;
            set => hash.Mode = value;
        }

        public PaddingMode Padding
        {
            get => hash.Padding;
            set => hash.Padding = value;
        }

        protected AbstractSymmetricHashAlgorithm(SymmetricAlgorithm algorithm) : base()
        {
            hash = algorithm;
            hash.GenerateKey();
            hash.GenerateIV();
        }

        //[Obsolete("Experimental")]
        //public CryptoStreamReader CreateStreamReader(Stream stream, bool leaveOpen)
        //{
        //    return new CryptoStreamReader(_hash.CreateDecryptor(), stream, Encoding.Unicode, leaveOpen);
        //}

        public sealed override byte[] Encrypt(byte[] value)
        {
            using (var memStream = new MemoryStream())
            using (var transform = hash.CreateEncryptor())
            using (var cryptoStream = new CryptoStream(memStream, transform, CryptoStreamMode.Write))
            {
                cryptoStream.Write(value, 0, value.Length);
                cryptoStream.Flush();
                cryptoStream.FlushFinalBlock();
                memStream.Flush();
                return memStream.ToArray();
            }
        }

        protected override string DoDecrypt(string value, Encoding encoding)
        {
            #warning this is not good code!
            var bytes = Convert.FromBase64String(value);
            return encoding.GetString(DoDecrypt(bytes));
        }
        
        protected override byte[] DoDecrypt(byte[] value)
        {
            using (var memStream = new MemoryStream(value))
            using (var decryptor = hash.CreateDecryptor())
            using (var cryptoStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read))
            {
                // Since at this point we don't know what the size of decrypted data
                // will be, allocate the buffer long enough to hold ciphertext;
                // plaintext is never longer than ciphertext.
                var length = value.Length;
                var plainTextBytes = new byte[length];
                var decryptedByteLength = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
              
                var result = new byte[decryptedByteLength];
                Array.ConstrainedCopy(plainTextBytes, 0, result, 0, decryptedByteLength);
                return result;
            }
            //using (var reader = CreateStreamReader(memStream, false))
            //{
            //    var length = value.Length;
            //    var plainTextBytes = new byte[length];
            //    var decryptedByteLength = reader.Read(
            //        plainTextBytes,
            //        0,
            //        plainTextBytes.Length);

            //    var result = new byte[decryptedByteLength];
            //    Array.ConstrainedCopy(plainTextBytes, 0, result, 0, decryptedByteLength);
            //    return result;
            //}
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && hash is IDisposable disposableHash)
            {
                disposableHash.Dispose();
            }
        }
    }
}