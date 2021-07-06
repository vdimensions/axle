using System.Security.Cryptography;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    public sealed class Sha1ManagedHashAlgorithm : AbstractHashAlgorithm
    {
        public Sha1ManagedHashAlgorithm() : base(new SHA1Managed()) { }
    }
}