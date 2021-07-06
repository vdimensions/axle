using System.Security.Cryptography;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    public sealed class Sha1HashAlgorithm : AbstractHashAlgorithm
    {
        public Sha1HashAlgorithm() : base(SHA1.Create()) { }
    }
}