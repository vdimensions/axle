#if !NETSTANDARD
using System.Security.Cryptography;

using Axle.Security.Cryptography.Algorithms.Hash.Sdk;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class Sha512CngHashAlgorithm : AbstractHashAlgorithm
    {
        public Sha512CngHashAlgorithm() : base(new SHA512Cng()) { }
    }
}
#endif