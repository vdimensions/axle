using System.Security.Cryptography;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    public sealed class Sha1NativeHashAlgorithm : AbstractHashAlgorithm
    {
        public Sha1NativeHashAlgorithm() : base(new SHA1CryptoServiceProvider()) { }
    }
}