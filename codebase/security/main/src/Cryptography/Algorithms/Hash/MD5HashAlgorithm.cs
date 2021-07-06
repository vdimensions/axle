using System.Security.Cryptography;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    public sealed class MD5HashAlgorithm : AbstractHashAlgorithm
    {
        public MD5HashAlgorithm() : base(MD5.Create()) { }
    }
}