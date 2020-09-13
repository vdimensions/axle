#if NETFRAMEWORK && NET35_OR_NEWER
using System.Security.Cryptography;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    public sealed class Sha384CngHashAlgorithm : AbstractHashAlgorithm
    {
        public Sha384CngHashAlgorithm() : base(new SHA384Cng()) { }
    }
}
#endif