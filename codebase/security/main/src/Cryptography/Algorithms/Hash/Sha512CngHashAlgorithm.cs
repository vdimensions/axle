#if NETFRAMEWORK && NET35_OR_NEWER
using System.Security.Cryptography;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class Sha512CngHashAlgorithm : AbstractHashAlgorithm
    {
        public Sha512CngHashAlgorithm() : base(new SHA512Cng()) { }
    }
}
#endif