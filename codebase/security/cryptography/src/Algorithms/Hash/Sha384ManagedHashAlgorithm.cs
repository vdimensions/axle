using System.Security.Cryptography;

using Axle.Security.Cryptography.Algorithms.Hash.Sdk;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class Sha384ManagedHashAlgorithm : AbstractHashAlgorithm
    {
        public Sha384ManagedHashAlgorithm() : base(new SHA384Managed()) { }
    }
}