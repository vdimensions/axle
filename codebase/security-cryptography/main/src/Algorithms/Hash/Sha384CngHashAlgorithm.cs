#if !NETSTANDARD
using System.Security.Cryptography;

using Axle.Security.Cryptography.Algorithms.Hash.Sdk;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class Sha384CngHashAlgorithm : AbstractHashAlgorithm
    {
        public Sha384CngHashAlgorithm() : base(new SHA384Cng()) { }
    }
}
#endif