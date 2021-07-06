using System.Security.Cryptography;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    public sealed class Sha256HashAlgorithm : AbstractHashAlgorithm
    {
        public Sha256HashAlgorithm() : base(SHA256.Create()) { }
    }
}