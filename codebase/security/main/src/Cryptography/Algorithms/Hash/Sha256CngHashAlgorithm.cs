#if NETFRAMEWORK && NET35_OR_NEWER
using System.Security.Cryptography;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class Sha256CngHashAlgorithm : AbstractHashAlgorithm
    {
        public Sha256CngHashAlgorithm() : base(new SHA256Cng()) { }
    }
}
#endif