#if !NETSTANDARD
using System.Security.Cryptography;
using Axle.Security.Cryptography.Algorithms.Hash.Sdk;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class MD5CngHashAlgorithm : AbstractHashAlgorithm
    {
        public MD5CngHashAlgorithm() : base(new MD5Cng()) { }
    }
}
#endif