using System.Security.Cryptography;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    public sealed class Sha512ManagedHashAlgorithm : AbstractHashAlgorithm
    {
        public Sha512ManagedHashAlgorithm() : base(new SHA512Managed()) { }
    }
}