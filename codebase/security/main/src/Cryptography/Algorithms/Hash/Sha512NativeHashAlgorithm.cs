#if NETSTANDARD2_0_OR_NEWER || NET35_OR_NEWER
using System.Security.Cryptography;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class Sha512NativeHashAlgorithm : AbstractHashAlgorithm
    {
        public Sha512NativeHashAlgorithm() : base(new SHA512CryptoServiceProvider()) { }
    }
}
#endif