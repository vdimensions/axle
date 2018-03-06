#if !NETSTANDARD
using System.Security.Cryptography;
using Axle.Security.Cryptography.Algorithms.Hash.Sdk;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class Sha1CngHashAlgorithm : AbstractHashAlgorithm
    {
        public Sha1CngHashAlgorithm() : base(new SHA1Cng()) { }
    }
}
#endif