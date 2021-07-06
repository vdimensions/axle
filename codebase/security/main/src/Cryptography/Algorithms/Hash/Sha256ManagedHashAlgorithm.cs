using System.Security.Cryptography;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    public sealed class Sha256ManagedHashAlgorithm : AbstractHashAlgorithm
    {
        public Sha256ManagedHashAlgorithm() : base(new SHA256Managed()) { }
    }
}