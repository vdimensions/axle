using System.Security.Cryptography;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    public sealed class Sha384ManagedHashAlgorithm : AbstractHashAlgorithm
    {
        public Sha384ManagedHashAlgorithm() : base(new SHA384Managed()) { }
    }
}