using System.Security.Cryptography;

using Axle.Security.Cryptography.Algorithms.Hash.Sdk;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class Sha384ManagedHashAlgorithm : AbstractHashAlgorithm
    {
        public Sha384ManagedHashAlgorithm() : base(new SHA384Managed()) { }
    }
}