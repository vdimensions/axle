#if NETFRAMEWORK
using System.Security.Cryptography;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class RIPEMD160HashAlgorithm : AbstractHashAlgorithm
    {
        public RIPEMD160HashAlgorithm() : base(new RIPEMD160Managed()) { }
    }
}
#endif