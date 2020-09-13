#if NETFRAMEWORK && NET35_OR_NEWER
using System.Security.Cryptography;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    public sealed class Sha512CngHashAlgorithm : AbstractHashAlgorithm
    {
        public Sha512CngHashAlgorithm() : base(new SHA512Cng()) { }
    }
}
#endif