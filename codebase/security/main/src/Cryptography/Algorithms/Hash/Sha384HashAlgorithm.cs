using System.Security.Cryptography;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class Sha384HashAlgorithm : AbstractHashAlgorithm
    {
        public Sha384HashAlgorithm() : base(SHA384.Create()) { }
    }
}