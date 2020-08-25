using System.Security.Cryptography;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class MD5NativeHashAlgorithm : AbstractHashAlgorithm
    {
        public MD5NativeHashAlgorithm() : base(new MD5CryptoServiceProvider()) { }
    }
}