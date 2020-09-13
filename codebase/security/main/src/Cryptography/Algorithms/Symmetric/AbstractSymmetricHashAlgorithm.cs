using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Axle.Security.Cryptography.Algorithms.Symmetric
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public abstract class AbstractSymmetricHashAlgorithm : AbstractCryptographicAlgorithm, ISymmetricHashAlgorithm
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly SymmetricAlgorithm _hash;

        public override ICryptoTransform CreateEncryptor() => _hash.CreateEncryptor();
        public override ICryptoTransform CreateDecryptor() => _hash.CreateDecryptor();

        /// <summary>
        /// Gets or sets the block size, in bits, of the cryptographic operation.
        /// </summary>
        public int BlockSize
        {
            get => _hash.BlockSize;
            set => _hash.BlockSize = value;
        }

        /// <summary>
        /// Gets or sets the initialization vector (IV) for the symmetric algorithm.
        /// </summary>
        public byte[] IV
        {
            get => _hash.IV;
            set => _hash.IV = value;
        }

        /// <summary>
        /// Gets or sets the secret key for the symmetric algorithm.
        /// </summary>
        public byte[] Key
        {
            get => _hash.Key;
            set => _hash.Key = value;
        }

        /// <summary>
        /// Gets or sets the size, in bits, of the secret key used by the symmetric algorithm.
        /// </summary>
        public int KeySize
        {
            get => _hash.KeySize;
            set => _hash.KeySize = value;
        }

        /// <summary>
        /// Gets the block sizes, in bits, that are supported by the symmetric algorithm.
        /// </summary>
        public KeySizes[] LegalBlockKeySizes => _hash.LegalBlockSizes;

        /// <summary>
        /// Gets the key sizes, in bits, that are supported by the symmetric algorithm.
        /// </summary>
        public KeySizes[] LegalKeySizes => _hash.LegalKeySizes;

        public CipherMode Mode
        {
            get => _hash.Mode;
            set => _hash.Mode = value;
        }

        /// <summary>
        /// Gets or sets the padding mode used in the symmetric algorithm.
        /// </summary>
        public PaddingMode Padding
        {
            get => _hash.Padding;
            set => _hash.Padding = value;
        }

        protected AbstractSymmetricHashAlgorithm(SymmetricAlgorithm algorithm) : base()
        {
            _hash = algorithm;
            _hash.GenerateKey();
            _hash.GenerateIV();
        }

        //[Obsolete("Experimental")]
        //public CryptoStreamReader CreateStreamReader(Stream stream, bool leaveOpen)
        //{
        //    return new CryptoStreamReader(_hash.CreateDecryptor(), stream, Encoding.Unicode, leaveOpen);
        //}

        public sealed override byte[] Encrypt(byte[] value)
        {
            using (var memStream = new MemoryStream())
            using (var transform = _hash.CreateEncryptor())
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
            using (var decryptor = _hash.CreateDecryptor())
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
            if (disposing && _hash is IDisposable disposableHash)
            {
                disposableHash.Dispose();
            }
        }
    }
}