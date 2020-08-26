using System.Security.Cryptography;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class Sha1HashAlgorithm : AbstractHashAlgorithm
    {
        public Sha1HashAlgorithm() : base(SHA1.Create()) { }
    }
}