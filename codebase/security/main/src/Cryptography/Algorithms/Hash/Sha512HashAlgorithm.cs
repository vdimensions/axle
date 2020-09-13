using System.Security.Cryptography;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    public sealed class Sha512HashAlgorithm : AbstractHashAlgorithm
    {
        public Sha512HashAlgorithm() : base(SHA512.Create()) { }
    }
}