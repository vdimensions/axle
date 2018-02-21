using System;
using System.Diagnostics;
using System.Security.Cryptography;

using Axle.Security.Cryptography.Algorithms.Sdk;


namespace Axle.Security.Cryptography.Algorithms.Hash.Sdk
{
    #if !NETSTANDARD
    [Serializable]
    #endif
    public abstract class AbstractHashAlgorithm : AbstractEncryptionAlgorithm, IHashAlgorithm
    {
        private sealed class HashCryptoTransform : CryptoTransformProxy, IHashCryptoTransform
        {
            private readonly System.Security.Cryptography.HashAlgorithm hash;

            public HashCryptoTransform(System.Security.Cryptography.HashAlgorithm target) : base(target)
            {
                this.hash = target;
            }

            public byte[] Hash => hash.Hash;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly System.Security.Cryptography.HashAlgorithm hash;

        protected AbstractHashAlgorithm(System.Security.Cryptography.HashAlgorithm algorithm)
        {
            hash = algorithm;
        }

        public override ICryptoTransform CreateEncryptor() => new HashCryptoTransform(hash);
        IHashCryptoTransform IHashAlgorithm.CreateEncryptor() => CreateEncryptor() as IHashCryptoTransform;

        public override ICryptoTransform CreateDecryptor() => new HashCryptoTransform(hash);
        IHashCryptoTransform IHashAlgorithm.CreateDecryptor() => CreateDecryptor() as IHashCryptoTransform;

        public override byte[] Encrypt(byte[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            return hash.ComputeHash(value);
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
