#if NETFRAMEWORK && NET35_OR_NEWER
using System.Security.Cryptography;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class MD5CngHashAlgorithm : AbstractHashAlgorithm
    {
        public MD5CngHashAlgorithm() : base(new MD5Cng()) { }
    }
}
#endif