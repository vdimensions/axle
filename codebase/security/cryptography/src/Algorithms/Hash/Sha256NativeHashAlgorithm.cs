using System.Security.Cryptography;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class Sha256NativeHashAlgorithm : AbstractHashAlgorithm
    {
        public Sha256NativeHashAlgorithm() : base(new SHA256CryptoServiceProvider()) { }
    }
}