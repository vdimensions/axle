#if NETFRAMEWORK
using System.Security.Cryptography;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    public sealed class RIPEMD160HashAlgorithm : AbstractHashAlgorithm
    {
        public RIPEMD160HashAlgorithm() : base(new RIPEMD160Managed()) { }
    }
}
#endif