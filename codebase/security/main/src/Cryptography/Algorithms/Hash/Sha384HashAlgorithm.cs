using System.Security.Cryptography;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    public sealed class Sha384HashAlgorithm : AbstractHashAlgorithm
    {
        public Sha384HashAlgorithm() : base(SHA384.Create()) { }
    }
}