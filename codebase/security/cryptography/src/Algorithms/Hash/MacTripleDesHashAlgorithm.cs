#if !NETSTANDARD
using System.Security.Cryptography;
using Axle.Security.Cryptography.Algorithms.Hash.Sdk;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class MacTripleDesHashAlgorithm : AbstractHashAlgorithm
    {
        public MacTripleDesHashAlgorithm() : base(new MACTripleDES()) { }
    }
}
#endif