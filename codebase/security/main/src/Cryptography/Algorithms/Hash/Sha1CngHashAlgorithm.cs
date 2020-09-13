#if NETFRAMEWORK && NET35_OR_NEWER
using System.Security.Cryptography;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    public sealed class Sha1CngHashAlgorithm : AbstractHashAlgorithm
    {
        public Sha1CngHashAlgorithm() : base(new SHA1Cng()) { }
    }
}
#endif