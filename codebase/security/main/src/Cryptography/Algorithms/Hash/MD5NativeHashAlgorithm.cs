using System.Security.Cryptography;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    public sealed class MD5NativeHashAlgorithm : AbstractHashAlgorithm
    {
        public MD5NativeHashAlgorithm() : base(new MD5CryptoServiceProvider()) { }
    }
}