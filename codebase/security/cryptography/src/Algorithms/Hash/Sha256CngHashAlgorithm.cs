#if NETFRAMEWORK
using System.Security.Cryptography;
using Axle.Security.Cryptography.Algorithms.Hash.Sdk;

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