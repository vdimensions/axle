#if !NETSTANDARD
using System.Security.Cryptography;
using Axle.Security.Cryptography.Algorithms.Hash.Sdk;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class Sha256CngHashAlgorithm : AbstractHashAlgorithm
    {
        public Sha256CngHashAlgorithm() : base(new SHA256Cng()) { }
    }
}
#endif