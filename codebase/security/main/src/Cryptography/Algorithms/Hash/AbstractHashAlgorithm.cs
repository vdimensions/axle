using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public abstract class AbstractHashAlgorithm : AbstractEncryptionAlgorithm, IHashAlgorithm
    {
        private sealed class HashCryptoTransform : CryptoTransformProxy, IHashCryptoTransform
        {
            private readonly System.Security.Cryptography.HashAlgorithm _hash;

            public HashCryptoTransform(System.Security.Cryptography.HashAlgorithm target) : base(target)
            {
                _hash = target;
            }

            public byte[] Hash => _hash.Hash;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly System.Security.Cryptography.HashAlgorithm _hash;

        protected AbstractHashAlgorithm(System.Security.Cryptography.HashAlgorithm algorithm)
        {
            _hash = algorithm;
        }

        public override ICryptoTransform CreateEncryptor() => new HashCryptoTransform(_hash);
        IHashCryptoTransform IHashAlgorithm.CreateEncryptor() => CreateEncryptor() as IHashCryptoTransform;

        public override ICryptoTransform CreateDecryptor() => new HashCryptoTransform(_hash);
        IHashCryptoTransform IHashAlgorithm.CreateDecryptor() => CreateDecryptor() as IHashCryptoTransform;

        public override byte[] Encrypt(byte[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            return _hash.ComputeHash(value);
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
