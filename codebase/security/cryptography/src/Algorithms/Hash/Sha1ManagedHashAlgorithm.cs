using System.Security.Cryptography;
using Axle.Security.Cryptography.Algorithms.Hash.Sdk;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class Sha1ManagedHashAlgorithm : AbstractHashAlgorithm
    {
        public Sha1ManagedHashAlgorithm() : base(new SHA1Managed()) { }
    }
}