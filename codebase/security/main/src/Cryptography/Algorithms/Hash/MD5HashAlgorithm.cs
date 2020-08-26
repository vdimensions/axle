using System.Security.Cryptography;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class MD5HashAlgorithm : AbstractHashAlgorithm
    {
        public MD5HashAlgorithm() : base(MD5.Create()) { }
    }
}